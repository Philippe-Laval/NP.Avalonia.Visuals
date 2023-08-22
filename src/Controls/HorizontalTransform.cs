using Avalonia;
using Avalonia.Animation;
using Avalonia.Media;
using NP.Avalonia.Visuals.Behaviors;
using System;
using System.Threading.Channels;

namespace NP.Avalonia.Visuals.Controls
{
    public class HorizontalTransform : MyScaleTransform
    {
        #region TheVisualFlow Styled Avalonia Property
        public VisualFlow TheVisualFlow
        {
            get { return GetValue(TheVisualFlowProperty); }
            set { SetValue(TheVisualFlowProperty, value); }
        }

        public static readonly StyledProperty<VisualFlow> TheVisualFlowProperty =
            AvaloniaProperty.Register<HorizontalTransform, VisualFlow>
            (
                nameof(TheVisualFlow)
            );
        #endregion TheVisualFlow Styled Avalonia Property

        public HorizontalTransform()
        {
            this.GetObservable(TheVisualFlowProperty).Subscribe(OnVisualFlowChanged);
        }

        private void OnVisualFlowChanged(VisualFlow visualFlow)
        {
            this.ScaleX = visualFlow.ToScale();
        }
    }

    /// <summary>
    /// Copied from ScaleTransform that was marked as sealed
    /// </summary>
    public class MyScaleTransform : Animatable, IMutableTransform
    {
        /// <summary>
        /// Defines the <see cref="ScaleX"/> property.
        /// </summary>
        public static readonly StyledProperty<double> ScaleXProperty =
                    AvaloniaProperty.Register<ScaleTransform, double>(nameof(ScaleX), 1);

        /// <summary>
        /// Defines the <see cref="ScaleY"/> property.
        /// </summary>
        public static readonly StyledProperty<double> ScaleYProperty =
                    AvaloniaProperty.Register<ScaleTransform, double>(nameof(ScaleY), 1);


        public MyScaleTransform()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleTransform"/> class.
        /// </summary>
        /// <param name="scaleX">ScaleX</param>
        /// <param name="scaleY">ScaleY</param>
        public MyScaleTransform(double scaleX, double scaleY) : base()
        {
            ScaleX = scaleX;
            ScaleY = scaleY;
        }

        /// <summary>
        /// Gets or sets the ScaleX property.
        /// </summary>
        public double ScaleX
        {
            get { return GetValue(ScaleXProperty); }
            set { SetValue(ScaleXProperty, value); }
        }

        /// <summary>
        /// Gets or sets the ScaleY property.
        /// </summary>
        public double ScaleY
        {
            get { return GetValue(ScaleYProperty); }
            set { SetValue(ScaleYProperty, value); }
        }

        /// <summary>
        /// Gets the transform's <see cref="Matrix"/>.
        /// </summary>
        public Matrix Value => Matrix.CreateScale(ScaleX, ScaleY);

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == ScaleXProperty || change.Property == ScaleYProperty)
            {
                RaiseChanged();
            }
        }

        /// <summary>
        /// Raised when the transform changes.
        /// </summary>
        public event EventHandler? Changed;

        protected void RaiseChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

    }
}
