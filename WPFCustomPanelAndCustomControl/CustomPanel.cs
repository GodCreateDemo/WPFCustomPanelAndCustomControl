using System.Windows;
using System.Windows.Controls;

namespace WPFCustomPanelAndCustomControl;

public class CustomPanel : Panel
{
    static CustomPanel()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomPanel),
            new FrameworkPropertyMetadata(typeof(CustomPanel)));
    }

    /// <summary>
    ///     在派生类中重写时，测量子元素在布局中所需的大小，并确定由 FrameworkElement 派生的类的大小。
    /// </summary>
    /// <param name="availableSize">
    ///     此元素可提供给子元素的可用大小。 可指定无穷大作为一个值，该值指示元素将调整到适应内容的大小。
    /// </param>
    /// <returns></returns>
    protected override Size MeasureOverride(Size availableSize)
    {
        var width = availableSize.Width;
        var height = availableSize.Height;
        var count = InternalChildren.Count;
        width /= count;
        for (var index = 0; index < count; index++)
        {
            var child = (Control)InternalChildren[index];
            //更新 UIElement 的 DesiredSize。
            //父元素从其自身的 MeasureCore(Size) 实现调用此方法以形成递归布局更新。
            //调用此方法构成布局更新的第一个处理过程（“测量”处理过程）。
            child.Measure(new Size(width, height));
        }

        return availableSize;
    }

    /// <summary>
    ///     在派生类中重写时，为 FrameworkElement 派生类定位子元素并确定大小。
    /// </summary>
    /// <param name="finalSize">
    ///     父级中应使用此元素排列自身及其子元素的最终区域。
    /// </param>
    /// <returns></returns>
    protected override Size ArrangeOverride(Size finalSize)
    {
        var height = finalSize.Height;
        var width = finalSize.Width;
        var count = InternalChildren.Count;
        width /= count;
        for (var index = 0; index < count; index++)
        {
            var child = (Control)InternalChildren[index];
            //定位子元素，并确定 UIElement 的大小。
            //父元素从它们的 ArrangeCore(Rect) 实现（或者是 WPF 框架级别等效项）调用此方法，以便形成递归布局更新。
            //此方法产生第二次布局更新。
            child.Arrange(new Rect(width * index, 0, child.DesiredSize.Width, height));
        }

        return finalSize;
    }
}