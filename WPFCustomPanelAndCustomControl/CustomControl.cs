using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFCustomPanelAndCustomControl;

public class CustomControl : Control
{
    public static readonly DependencyProperty FillProperty =
        DependencyProperty.Register("Fill", typeof(Brush), typeof(CustomControl),
            new PropertyMetadata(default(Brush?)));

    private readonly Brush[] _brushes;

    private readonly int _count;

    private readonly Random _random;

    static CustomControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomControl),
            new FrameworkPropertyMetadata(typeof(CustomControl)));
    }

    public CustomControl()
    {
        var type = typeof(Brushes);
        var brushes = type.GetProperties((BindingFlags)int.MaxValue)
            .Where(x =>
                x.GetMethod is var methodInfo and not null
                && methodInfo.IsPublic
                && methodInfo.IsStatic
                && typeof(Brush).IsAssignableFrom(x.PropertyType))
            .Select(x => (Brush)x.GetValue(default)!)
            .ToArray();
        _count = brushes.Length;
        _brushes = brushes;
        var random = new Random();
        _random = random;
    }

    public Brush? Fill
    {
        get => (Brush?)GetValue(FillProperty);
        set => SetValue(FillProperty, value);
    }

    protected override Size MeasureOverride(Size constraint)
    {
        return constraint;
    }

    protected override Size ArrangeOverride(Size arrangeBounds)
    {
        return arrangeBounds;
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        Fill ??= 获取随机颜色();
        drawingContext.DrawRectangle(Fill, default, new Rect(new Point(), DesiredSize));
    }

    private Brush 获取随机颜色()
    {
        var next = _random.Next(0, _count);
        return _brushes[next];
    }
}