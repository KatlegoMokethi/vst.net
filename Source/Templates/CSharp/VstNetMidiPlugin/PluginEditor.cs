﻿using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using Jacobi.Vst.Plugin.Framework.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using VstNetMidiPlugin.UI;

namespace VstNetMidiPlugin
{
    /// <summary>
    /// This object manages the custom editor (UI) for your plugin.
    /// </summary>
    /// <remarks>
    /// When you do not implement a custom editor, 
    /// your Parameters will be displayed in an editor provided by the host.
    /// </remarks>
    internal sealed class PluginEditor : IVstPluginEditor
    {
        private Plugin _plugin;
        private WinFormsControlWrapper<PluginEditorView> _view;

        public PluginEditor(Plugin plugin)
        {
            _plugin = plugin;
            _view = new WinFormsControlWrapper<PluginEditorView>();
        }

        public Rectangle Bounds
        {
            get { return _view.Bounds; }
        }

        public void Close()
        {
            _view.Close();
        }

        public bool KeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            // empty by design
            return false;
        }

        public bool KeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            // empty by design
            return false;
        }

        public VstKnobMode KnobMode { get; set; }

        public void Open(IntPtr hWnd)
        {
            // make a list of parameters to pass to the dlg.
            var paramList = new List<VstParameterManager>()
                {
                    _plugin.MidiProcessor.Gain.GainMgr,
                    _plugin.MidiProcessor.Transpose.TransposeMgr,
                };

            _view.SafeInstance.InitializeParameters(paramList);

            _view.Open(hWnd);
        }

        public void ProcessIdle()
        {
            // keep your processing short!
            _view.SafeInstance.ProcessIdle();
        }
    }
}
