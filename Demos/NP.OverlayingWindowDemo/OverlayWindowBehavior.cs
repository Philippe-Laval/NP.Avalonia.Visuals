﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;
using NP.Avalonia.Visuals;
using NP.Utilities;
using System;

namespace NP.OverlayingWindowDemo
{
    public class OverlayWindowBehavior
    {
        #region IsOpen Attached Avalonia Property
        public static bool GetIsOpen(IControl obj)
        {
            return obj.GetValue(IsOpenProperty);
        }

        public static void SetIsOpen(IControl obj, bool value)
        {
            obj.SetValue(IsOpenProperty, value);
        }

        public static readonly AttachedProperty<bool> IsOpenProperty =
            AvaloniaProperty.RegisterAttached<OverlayWindowBehavior, IControl, bool>
            (
                "IsOpen"
            );
        #endregion IsOpen Attached Avalonia Property

        #region Padding Attached Avalonia Property
        public static Thickness GetPadding(IControl obj)
        {
            return obj.GetValue(PaddingProperty);
        }

        public static void SetPadding(IControl obj, Thickness value)
        {
            obj.SetValue(PaddingProperty, value);
        }

        public static readonly AttachedProperty<Thickness> PaddingProperty =
            AvaloniaProperty.RegisterAttached<OverlayWindowBehavior, IControl, Thickness>
            (
                "Padding"
            );
        #endregion Padding Attached Avalonia Property


        #region Content Attached Avalonia Property
        public static object GetContent(IControl obj)
        {
            return obj.GetValue(ContentProperty);
        }

        public static void SetContent(IControl obj, object value)
        {
            obj.SetValue(ContentProperty, value);
        }

        public static readonly AttachedProperty<object> ContentProperty =
            AvaloniaProperty.RegisterAttached<OverlayWindowBehavior, IControl, object>
            (
                "Content"
            );
        #endregion Content Attached Avalonia Property


        #region ContentTemplate Attached Avalonia Property
        public static DataTemplate GetContentTemplate(IControl obj)
        {
            return obj.GetValue(ContentTemplateProperty);
        }

        public static void SetContentTemplate(IControl obj, DataTemplate value)
        {
            obj.SetValue(ContentTemplateProperty, value);
        }

        public static readonly AttachedProperty<DataTemplate> ContentTemplateProperty =
            AvaloniaProperty.RegisterAttached<OverlayWindowBehavior, IControl, DataTemplate>
            (
                "ContentTemplate"
            );
        #endregion ContentTemplate Attached Avalonia Property


        #region OverlayWindow Attached Avalonia Property
        private static Window GetOverlayWindow(IControl obj)
        {
            return obj.GetValue(OverlayWindowProperty);
        }

        private static void SetOverlayWindow(IControl obj, Window value)
        {
            obj.SetValue(OverlayWindowProperty, value);
        }

        private static readonly AttachedProperty<Window> OverlayWindowProperty =
            AvaloniaProperty.RegisterAttached<OverlayWindowBehavior, IControl, Window>
            (
                "OverlayWindow"
            );
        #endregion OverlayWindow Attached Avalonia Property


        #region IsTopmost Attached Avalonia Property
        public static bool GetIsTopmost(IControl obj)
        {
            return obj.GetValue(IsTopmostProperty);
        }

        public static void SetIsTopmost(IControl obj, bool value)
        {
            obj.SetValue(IsTopmostProperty, value);
        }

        public static readonly AttachedProperty<bool> IsTopmostProperty =
            AvaloniaProperty.RegisterAttached<OverlayWindowBehavior, IControl, bool>
            (
                "IsTopmost",
                true
            );
        #endregion IsTopmost Attached Avalonia Property


        static OverlayWindowBehavior()
        {
            IsOpenProperty.Changed.Subscribe(OnIsOpenChanged);
        }

        private static void OnIsOpenChanged(AvaloniaPropertyChangedEventArgs<bool> args)
        {
            IControl control = (IControl) args.Sender;

            Window? overlayWindow = GetOverlayWindow(control);

            if (args.NewValue.Value)
            {
                if (overlayWindow == null)
                {
                    overlayWindow =
                        new Window()
                        {
                            TransparencyLevelHint = WindowTransparencyLevel.Transparent,
                            Background = null,
                            SystemDecorations = SystemDecorations.None,
                            Topmost = GetIsTopmost(control),
                            Content = GetContent(control),
                            ContentTemplate = GetContentTemplate(control),
                            Padding = GetPadding(control)
                        };

                    SetOverlayWindow(control, overlayWindow);
                }

                Rect2D screenBounds = control.GetScreenBounds();

                double scale = 1d / overlayWindow.PlatformImpl.RenderScaling;
                overlayWindow.Position = screenBounds.StartPoint.ToPixelPoint();
                overlayWindow.PlatformImpl.Resize(screenBounds.GetSize(scale).ToSize());

                overlayWindow.Show();
            }
            else
            {
                overlayWindow?.Hide();
            }
        }
    }
}
