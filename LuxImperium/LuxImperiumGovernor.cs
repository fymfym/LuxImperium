using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using LuxImperium.Models;
using LuxImperium.Services;
using LuxImperium.Services.Actions;
using Newtonsoft.Json;

namespace LuxImperium
{
    public class LuxImperiumGovernor
    {
        private readonly IActionFactory _actionFactory;
        private readonly List<IAction> _actions = new List<IAction>();
        private readonly Timer _sendTimer;

        private DmxScene _latestLoadedScene;
        private IAction _startAction;
        private IAction _stopAction;
        
        public LuxImperiumGovernor(
            IActionFactory actionFactory
            )
        {
            _actionFactory = actionFactory;
            _latestLoadedScene = new DmxScene();
            _sendTimer = new Timer();
            _sendTimer.Elapsed += SendTimerOnElapsed;
            _sendTimer.Interval = 25;
        }

        public bool Running => _sendTimer.Enabled;
        
        private void SendTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            var sceneActions = RunActions();
            foreach (var sceneAction in sceneActions)
            {
                OpenDmx.SetDmxValue(sceneAction.Channel, sceneAction.Value); 
            }            
            OpenDmx.WriteData();
        }

        public void LoadScene(string filename)
        {
            if (_sendTimer.Enabled)
            {
                throw new InvalidProgramException("DMX sending data, new scenes can not be loaded");
            }
            
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("Scene file not found", filename);
            }

            var fileData = File.ReadAllText(filename);

            _latestLoadedScene = JsonConvert.DeserializeObject<DmxScene>(fileData);
            
            if (_latestLoadedScene == null)
            {
                throw new InvalidDataException("Scene file is not valid");
            }

            if (_latestLoadedScene.Actions == null)
            {
                return;
            }
            
            foreach (var dmxAction in _latestLoadedScene.Actions)
            {
                if (_latestLoadedScene.Fixtures.FirstOrDefault(x => x.Name.Equals(dmxAction.Fixture, StringComparison.OrdinalIgnoreCase)) == null)
                {
                    throw new InvalidDataException($"Action fixture {dmxAction.Fixture} not found in scene file");
                }

                var act = _actionFactory.Build(dmxAction);
                if (act != null)
                {
                    _actions.Add(act);
                }
            }

            if (_latestLoadedScene.StartAction != null)
            {
                _startAction = _actionFactory.Build(_latestLoadedScene.StartAction);
            }

            if (_latestLoadedScene.StopAction != null)
            {
                _stopAction = _actionFactory.Build(_latestLoadedScene.StopAction);
            }
            
        }

        public void Start()
        {
            if (_sendTimer.Enabled)
            {
                throw new InvalidProgramException("Enttec Open DMX USB all ready started");
            }
            
            OpenDmx.Start();
            
            if ( OpenDmx.Status == FtStatus.FT_DEVICE_NOT_FOUND)       //update status
                throw new InvalidOperationException("No Enttec USB Device Found");
            if (OpenDmx.Status != FtStatus.FT_OK)
                throw new InvalidOperationException("Error Opening Device");
            
            if (_startAction == null)
            {
                return;
            }

            var executeResult = _startAction.Execute();
            foreach (var sceneAction in executeResult)
            {
                OpenDmx.SetDmxValue(sceneAction.Channel, sceneAction.Value); 
            }
            OpenDmx.WriteData();

            _sendTimer.Enabled = true;
            
        }

        public void Stop()
        {
            if (_sendTimer.Enabled)
            {
                _sendTimer.Enabled = false;
            }
            
            System.Threading.Thread.Sleep(50);
            
            if (_stopAction != null)
            {
                var executeResult = _stopAction.Execute();
                foreach (var sceneAction in executeResult)
                {
                    OpenDmx.SetDmxValue(sceneAction.Channel, sceneAction.Value); 
                }
                OpenDmx.WriteData();
            }

            System.Threading.Thread.Sleep(500);
            OpenDmx.Stop();
        }
        
        public List<ActionExecuteResult> RunActions()
        {
            var result = new  List<ActionExecuteResult>();
            foreach (var action in _actions)
            {
                result.AddRange(action.Execute());
            }

            return result;
        }
        
    }
}