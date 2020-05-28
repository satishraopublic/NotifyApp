using System;
using System.Diagnostics;
using System.Windows.Threading;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;

namespace NotifyApp.Automation
{
    public class AutomationHelper
    {
        private readonly AutomationBase _automation;
        private readonly DispatcherTimer _dispatcherTimer;
        private AutomationElement _currentHoveredElement;

        public event Action<AutomationElement> ElementHovered;
        public event Action<AutomationElement> ControlReleasedOverElement;

        public AutomationHelper(AutomationBase automation)
        {
            _automation = automation;
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += DispatcherTimerTick;
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(500);
        }

        public void Start()
        {
            _currentHoveredElement = null;
            _dispatcherTimer.Start();
        }

        public void Stop()
        {
            _currentHoveredElement = null;
            _dispatcherTimer.Stop();
        }

        bool started = false;
        private void DispatcherTimerTick(object sender, EventArgs e)
        {
            
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                if (System.Windows.Input.Keyboard.Modifiers.HasFlag(System.Windows.Input.ModifierKeys.Control))
                {
                    started = true;
                    var screenPos = Mouse.Position;
                    var hoveredElement = _automation.FromPoint(screenPos);
                    // Skip items in the current process
                    // Like Inspect itself or the overlay window
                    if (hoveredElement.Properties.ProcessId == Process.GetCurrentProcess().Id)
                    {
                        return;
                    }
                    if (!Equals(_currentHoveredElement, hoveredElement))
                    {
                        _currentHoveredElement = hoveredElement;
                        ElementHovered?.Invoke(hoveredElement);
                    }
                    else
                    {
                        ElementHighlighter.HighlightElement(hoveredElement);
                    }
                }
                else
                {
                    if (started)
                    {
                        started = false;
                        var screenPos = Mouse.Position;
                        var hoveredElement = _automation.FromPoint(screenPos);
                        ControlReleasedOverElement?.Invoke(hoveredElement);
                    }
                }
            });
        }
    }
}
