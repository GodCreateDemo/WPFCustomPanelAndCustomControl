# Measure

Updates the [DesiredSize](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.desiredsize?view=windowsdesktop-6.0#system-windows-uielement-desiredsize) of a [UIElement](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement?view=windowsdesktop-6.0). Parent elements call this method from their own [MeasureCore(Size)](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.measurecore?view=windowsdesktop-6.0#system-windows-uielement-measurecore(system-windows-size)) implementations to form a recursive layout update. Calling this method constitutes the first pass (the "Measure" pass) of a layout update.

更新UIElement的所需大小。父元素从其自己的MeasureCore（Size）实现调用此方法，以形成递归布局更新。调用此方法构成布局更新的第一个过程（“Measure”过程）。

Computation of layout positioning in Windows Presentation Foundation (WPF) is comprised of a [Measure](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.measure?view=windowsdesktop-6.0) call and an [Arrange](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.arrange?view=windowsdesktop-6.0) call. During the [Measure](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.measure?view=windowsdesktop-6.0) call, an element determines its size requirements by using an `availableSize` input. During the [Arrange](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.arrange?view=windowsdesktop-6.0) call, the element size is finalized.

Windows Presentation Foundation（WPF）中布局定位的计算由度量调用和排列调用组成。在度量调用期间，元素通过使用availableSize输入来确定其大小要求。在Arrange调用期间，元素大小将最终确定。

`availableSize` can be any number from zero to infinite. Elements participating in layout should return the minimum [Size](https://docs.microsoft.com/en-us/dotnet/api/system.windows.size?view=windowsdesktop-6.0) they require for a given `availableSize`.

availableSize可以是从零到无限的任意数字。参与布局的元素应返回给定可用大小所需的最小大小。

When a layout is first instantiated, it always receives a [Measure](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.measure?view=windowsdesktop-6.0) call before [Arrange](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.arrange?view=windowsdesktop-6.0). However, after the first layout pass, it may receive an [Arrange](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.arrange?view=windowsdesktop-6.0) call without a [Measure](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.measure?view=windowsdesktop-6.0); this can happen when a property that affects only [Arrange](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.arrange?view=windowsdesktop-6.0) is changed (such as alignment), or when the parent receives an [Arrange](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.arrange?view=windowsdesktop-6.0) without a [Measure](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.measure?view=windowsdesktop-6.0). A [Measure](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.measure?view=windowsdesktop-6.0) call will automatically invalidate an [Arrange](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.arrange?view=windowsdesktop-6.0) call.

第一次实例化布局时，它总是在排列之前接收度量调用。然而，在第一次布局传递之后，它可能会收到一个没有措施的安排调用；如果更改了仅影响排列的属性（例如对齐），或者父级收到没有度量值的排列，则可能会发生这种情况。度量值调用将自动使排列调用无效。

Layout updates happen asynchronously, such that the main thread is not waiting for every possible layout change. Querying an element via code-behind checking of property values may not immediately reflect changes to properties that interact with the sizing or layout characteristics (the [Width](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.width?view=windowsdesktop-6.0) property, for example).

布局更新是异步进行的，因此主线程不会等待所有可能的布局更改。通过属性值的代码隐藏检查查询元素可能不会立即反映对与尺寸或布局特征（例如，宽度属性）交互的属性的更改。

> Note
>
> Layout updates can be forced by using the [UpdateLayout](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.updatelayout?view=windowsdesktop-6.0) method. However, calling this method is usually unnecessary and can cause poor performance.
>
> 注意
>
> 可以使用UpdateLayout方法强制进行布局更新。然而，调用此方法通常是不必要的，并且可能会导致性能不佳。

The layout system keeps two separate queues of invalid layouts, one for [Measure](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.measure?view=windowsdesktop-6.0) and one for [Arrange](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.arrange?view=windowsdesktop-6.0). The layout queue is sorted based upon the order of elements in the visual tree of the element performing layout; elements higher in the tree are at the top of the queue, to avoid redundant layouts caused by repeated changes in parents. Duplicate entries are automatically removed from the queue, and elements are automatically removed from the queue if they are already layout-validated.

布局系统保留两个单独的无效布局队列，一个用于度量，一个用于排列。布局队列根据执行布局的元素的可视化树中元素的顺序进行排序；树中较高的元素位于队列的顶部，以避免父级重复更改导致的冗余布局。重复条目将自动从队列中删除，如果元素已通过布局验证，则会自动从队列中删除。

When updating layout, the [Measure](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.measure?view=windowsdesktop-6.0) queue is emptied first, followed by the [Arrange](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.arrange?view=windowsdesktop-6.0) queue. An element in the [Arrange](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.arrange?view=windowsdesktop-6.0) queue will never be arranged if there is an element in the [Measure](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.measure?view=windowsdesktop-6.0) queue.

更新布局时，首先清空Measure队列，然后清空Arrange队列。如果Measure队列中有元素，则永远不会排列Arrange队列中的元素。

# 各种Size

## UIElement.DesiredSize Property

Gets the size that this element computed during the measure pass of the layout process.

获取此元素在布局过程的度量过程中计算的大小。

The value returned by this property will only be a valid measurement if the value of the [IsMeasureValid](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.ismeasurevalid?view=windowsdesktop-6.0) property is `true`.

仅当IsMeasureValid属性的值为true时，此属性返回的值才是有效的度量值。

[DesiredSize](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.desiredsize?view=windowsdesktop-6.0) is typically checked as one of the measurement factors when you implement layout behavior overrides such as [ArrangeOverride](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.arrangeoverride?view=windowsdesktop-6.0), [MeasureOverride](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.measureoverride?view=windowsdesktop-6.0), or [OnRender](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.onrender?view=windowsdesktop-6.0) (in the [OnRender](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.onrender?view=windowsdesktop-6.0) case, you might check [RenderSize](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.rendersize?view=windowsdesktop-6.0) instead, but this depends on your implementation). Depending on the scenario, [DesiredSize](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.desiredsize?view=windowsdesktop-6.0) might be fully respected by your implementation logic, constraints on [DesiredSize](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.desiredsize?view=windowsdesktop-6.0) might be applied, and such constraints might also change other characteristics of either the parent element or child element. For example, a control that supports scrollable regions (but chooses not to derive from the WPF framework-level controls that already enable scrollable regions) could compare available size to [DesiredSize](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.desiredsize?view=windowsdesktop-6.0). The control could then set an internal state that enabled scrollbars in the UI for that control. Or, [DesiredSize](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.desiredsize?view=windowsdesktop-6.0) could potentially also be ignored in certain scenarios.

当您实现布局行为替代（如ArrangeOverride、MeasureOverride或OnRender）时，DesiredSize通常作为度量因素之一进行检查（在OnRender情况下，您可以改为检查RenderSize，但这取决于您的实现）。根据场景的不同，DesiredSize可能会受到您的实现逻辑的充分尊重，可能会应用DesiredSize上的约束，并且这些约束也可能会更改父元素或子元素的其他特征。例如，支持可滚动区域的控件（但选择不从已启用可滚动区域的WPF框架级控件派生）可以将可用大小与DesiredSize进行比较。然后，该控件可以设置一个内部状态，以便在UI中为该控件启用滚动条。或者，DesiredSize也可能在某些场景中被忽略。

## UIElement.RenderSize Property

Gets (or sets) the final render size of this element.

获取（或设置）此元素的最终渲染大小。

> Important
>
> Do not attempt to set this property, either in XAML or in code, if using the WPF framework-level layout system. Nearly all typical application scenarios will use this layout system. The layout system will not respect sizes set in the [RenderSize](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.rendersize?view=windowsdesktop-6.0) property directly. The [RenderSize](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.rendersize?view=windowsdesktop-6.0) property is declared writable only to enable certain WPF core-level bridging cases that deliberately circumvent the typical layout protocols, such as support for the [Adorner](https://docs.microsoft.com/en-us/dotnet/api/system.windows.documents.adorner?view=windowsdesktop-6.0) class.
>
> 重要的
>
> 如果使用WPF框架级布局系统，请不要尝试在XAML或代码中设置此属性。几乎所有典型的应用场景都将使用此布局系统。布局系统不会直接考虑RenderSize属性中设置的大小。RenderSize属性声明为可写的，只是为了启用某些WPF核心级桥接情况，这些情况故意规避典型的布局协议，例如对Adorner类的支持。

This property can be used for checking the applicable render size within layout system overrides such as [OnRender](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.onrender?view=windowsdesktop-6.0) or [GetLayoutClip](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.getlayoutclip?view=windowsdesktop-6.0).

此属性可用于检查布局系统替代（如OnRender或GetLayoutClip）中适用的渲染大小。

A more common scenario is handling the [SizeChanged](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.sizechanged?view=windowsdesktop-6.0) event with the class handler override or the [OnRenderSizeChanged](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement.onrendersizechanged?view=windowsdesktop-6.0) event.

一种更常见的场景是使用类处理程序重写或OnRenderSizeChanged事件来处理SizeChanged事件。

## FrameworkElement.Width Property

Gets or sets the width of the element.

获取或设置元素的宽度。

This is one of three properties on [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement?view=windowsdesktop-6.0) that specify width information. The other two are [MinWidth](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.minwidth?view=windowsdesktop-6.0) and [MaxWidth](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.maxwidth?view=windowsdesktop-6.0). If there is a conflict between these values, the order of application for actual width determination is first [MinWidth](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.minwidth?view=windowsdesktop-6.0) must be honored, then [MaxWidth](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.maxwidth?view=windowsdesktop-6.0), and finally if each of these are within bounds, [Width](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.width?view=windowsdesktop-6.0).

这是FrameworkElement上指定宽度信息的三个属性之一。另外两个是MinWidth和MaxWidth。如果这些值之间存在冲突，则实际宽度确定的应用顺序是：首先必须遵循MinWidth，然后是MaxWidth，最后如果每个值都在范围内，则遵循width。

The return value of this property is always the same as any value that was set to it. In contrast, the value of the [ActualWidth](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.actualwidth?view=windowsdesktop-6.0) may vary. The layout may have rejected the suggested size for some reason. Also, the layout system itself works asynchronously relative to the property system set of [Width](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.width?view=windowsdesktop-6.0) and may not have processed that particular sizing property change yet.

此属性的返回值始终与设置给它的任何值相同。相反，实际宽度的值可能会有所不同。由于某种原因，布局可能拒绝了建议的大小。此外，布局系统本身相对于宽度的属性系统集异步工作，并且可能尚未处理该特定大小属性更改。

In addition to acceptable [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double?view=windowsdesktop-6.0) values, this property can also be [Double.NaN](https://docs.microsoft.com/en-us/dotnet/api/system.double.nan?view=windowsdesktop-6.0). This is how you specify auto sizing behavior. In XAML you set the value to the string "Auto" (case insensitive) to enable the auto sizing behavior. Auto sizing behavior implies that the element will fill the width available to it. Note however that specific controls frequently supply default values in their default styles that will disable the auto sizing behavior unless it is specifically re-enabled.

除了可接受的双精度值外，此属性还可以是Double.NaN。这就是指定自动调整大小行为的方式。在XAML中，将值设置为字符串“Auto”（不区分大小写），以启用自动调整大小行为。自动调整大小行为意味着元素将填充其可用的宽度。但是请注意，特定控件经常以其默认样式提供默认值，除非专门重新启用，否则将禁用自动调整大小行为。

In addition to the validation check, there is a nondeterministic upper value bound for [Width](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.width?view=windowsdesktop-6.0) that is enforced by the layout system (this is a very large number, larger than [Single.MaxValue](https://docs.microsoft.com/en-us/dotnet/api/system.single.maxvalue?view=windowsdesktop-6.0) but smaller than [Double.MaxValue](https://docs.microsoft.com/en-us/dotnet/api/system.double.maxvalue?view=windowsdesktop-6.0)). If you exceed this bound, the element will not render, and no exception is thrown. Do not set [Width](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.width?view=windowsdesktop-6.0) to a value that is significantly larger than the maximum size of any possible visual display, or you may exceed this nondeterministic upper bound.

除了验证检查之外，布局系统还强制执行一个不确定的宽度上限值（这是一个非常大的数字，大于Single.MaxValue，但小于Double.MaxValue）。如果超出此界限，则元素将不会呈现，并且不会引发异常。不要将宽度设置为明显大于任何可能的视觉显示的最大大小的值，否则可能会超过此不确定的上限。

## FrameworkElement.Height Property

Gets or sets the suggested height of the element.

获取或设置元素的建议高度。

[Height](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.height?view=windowsdesktop-6.0) is one of three writable properties on [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement?view=windowsdesktop-6.0) that specify height information. The other two are [MinHeight](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.minheight?view=windowsdesktop-6.0) and [MaxHeight](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.maxheight?view=windowsdesktop-6.0). If there is a conflict between these values, the order of application for actual height determination is that first [MinHeight](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.minheight?view=windowsdesktop-6.0) must be honored, then [MaxHeight](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.maxheight?view=windowsdesktop-6.0), and finally, if it is within bounds, [Height](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.height?view=windowsdesktop-6.0).

高度是FrameworkElement上指定高度信息的三个可写属性之一。另外两个是MinHeight和MaxHeight。如果这些值之间存在冲突，则实际高度确定的应用顺序是，首先必须遵守MinHeight，然后是MaxHeight，最后是height（如果在范围内）。

If this element is a child element within some other element, then setting this property to a value is really only a suggested value. The layout system as well as the particular layout logic of the parent element will use the value as a nonbinding input during the layout process. In practical terms, a [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement?view=windowsdesktop-6.0) is almost always the child element of something else; even when you set the [Height](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.height?view=windowsdesktop-6.0) on [Window](https://docs.microsoft.com/en-us/dotnet/api/system.windows.window?view=windowsdesktop-6.0). (For [Window](https://docs.microsoft.com/en-us/dotnet/api/system.windows.window?view=windowsdesktop-6.0), that value is used when the underlying application model establishes the basic rendering assumptions that create the Hwnd that hosts the application.)

如果此元素是其他元素中的子元素，则将此属性设置为值实际上只是建议的值。布局系统以及父元素的特定布局逻辑将在布局过程中使用该值作为非绑定输入。实际上，框架元素几乎总是其他元素的子元素；即使您在窗口上设置了高度。（对于Window，当基础应用程序模型建立创建承载应用程序的Hwnd的基本渲染假设时，将使用该值。）

In addition to acceptable [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double?view=windowsdesktop-6.0) values, this property can also be [Double.NaN](https://docs.microsoft.com/en-us/dotnet/api/system.double.nan?view=windowsdesktop-6.0). This is how you specify auto sizing behavior in code. In XAML you set the value to the string "Auto" (case insensitive) to enable the auto sizing behavior. Auto sizing behavior implies that the element will fill the height available to it. Note however that specific controls frequently supply default values through their default theme styles that will disable the auto sizing behavior unless it is specifically re-enabled.

除了可接受的双精度值外，此属性还可以是Double.NaN。这就是在代码中指定自动调整大小行为的方式。在XAML中，将值设置为字符串“Auto”（不区分大小写），以启用自动调整大小行为。自动调整大小行为意味着图元将填充其可用的高度。但是请注意，特定控件经常通过其默认主题样式提供默认值，除非专门重新启用，否则将禁用自动调整大小行为。

The return value of this property is always the same as any value that was set to it. In contrast, the value of the [ActualHeight](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.actualheight?view=windowsdesktop-6.0) may vary. This can happen either statically because the layout rejected the suggested size for some reason, or momentarily. The layout system itself works asynchronously relative to the property system's set of [Height](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.height?view=windowsdesktop-6.0) and may not have processed that particular sizing property change yet.

此属性的返回值始终与设置给它的任何值相同。相反，实际高度的值可能会有所不同。这可能是由于布局出于某种原因拒绝了建议的大小而静态发生的，也可能是暂时发生的。布局系统本身相对于属性系统的高度集异步工作，并且可能尚未处理特定大小属性的更改。

The value restrictions on the [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double?view=windowsdesktop-6.0) value are enforced by a [ValidateValueCallback](https://docs.microsoft.com/en-us/dotnet/api/system.windows.validatevaluecallback?view=windowsdesktop-6.0) mechanism. If you attempt to set an invalid value, a run-time exception is thrown.

对双精度值的值限制由ValidateValueCallback机制强制执行。如果试图设置无效值，将引发运行时异常。

In addition to the validation check, there is a nondeterministic upper value bound for [Height](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.height?view=windowsdesktop-6.0) that is enforced by the layout system (this is a very large number, larger than [Single.MaxValue](https://docs.microsoft.com/en-us/dotnet/api/system.single.maxvalue?view=windowsdesktop-6.0) but smaller than [Double.MaxValue](https://docs.microsoft.com/en-us/dotnet/api/system.double.maxvalue?view=windowsdesktop-6.0)). If you exceed this bound, the element will not render, and no exception is thrown. Do not set [Height](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.height?view=windowsdesktop-6.0) to a value that is significantly larger than the maximum size of any possible visual display, or you may exceed this nondeterministic upper bound.

除了验证检查之外，布局系统还强制执行高度的不确定性上限值（这是一个非常大的数字，大于Single.MaxValue，但小于Double.MaxValue）。如果超出此界限，则元素将不会呈现，并且不会引发异常。不要将高度设置为明显大于任何可能的视觉显示的最大大小的值，否则可能会超过此不确定的上限。

## FrameworkElement.ActualWidth Property

Gets the rendered width of this element.

获取此元素的渲染宽度。

This property is a calculated value based on other width inputs, and the layout system. The value is set by the layout system itself, based on an actual rendering pass, and may therefore lag slightly behind the set value of properties such as [Width](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.width?view=windowsdesktop-6.0) that are the basis of the input change.

此属性是基于其他宽度输入和布局系统计算的值。该值由布局系统本身根据实际渲染过程设置，因此可能会稍微滞后于作为输入更改基础的属性（如宽度）的设置值。

Because [ActualWidth](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.actualwidth?view=windowsdesktop-6.0) is a calculated value, you should be aware that there could be multiple or incremental reported changes to it as a result of various operations by the layout system. The layout system may be calculating required measure space for child elements, constraints by the parent element, and so on.

因为ActualWidth是一个计算值，所以您应该知道，由于布局系统的各种操作，可能会对其报告多个或增量的更改。布局系统可能正在计算子图元所需的度量空间、父图元的约束等。

Although you cannot set this property from XAML, you can base a [Trigger](https://docs.microsoft.com/en-us/dotnet/api/system.windows.trigger?view=windowsdesktop-6.0) upon its value in a style.

虽然不能从XAML设置此属性，但可以在样式中根据其值创建触发器。

##  FrameworkElement.ActualHeight Property

Gets the rendered height of this element.

获取此元素的渲染高度。

This property is a calculated value based on other height inputs, and the layout system. The value is set by the layout system itself, based on an actual rendering pass, and may therefore lag slightly behind the set value of properties such as [Height](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.height?view=windowsdesktop-6.0) that are the basis of the input change.

此特性是基于其他高度输入和布局系统计算的值。该值由布局系统本身根据实际渲染过程设置，因此可能会稍微滞后于作为输入更改基础的属性（如高度）的设置值。

Because [ActualHeight](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.actualheight?view=windowsdesktop-6.0) is a calculated value, you should be aware that there could be multiple or incremental reported changes to it as a result of various operations by the layout system. The layout system may be calculating required measure space for child elements, constraints by the parent element, and so on.

因为实际高度是一个计算值，所以您应该知道，由于布局系统的各种操作，可能会对其报告多个或增量的更改。布局系统可能正在计算子图元所需的度量空间、父图元的约束等。

Although you cannot set this property from XAML, you can base a [Trigger](https://docs.microsoft.com/en-us/dotnet/api/system.windows.trigger?view=windowsdesktop-6.0) upon its value in a style.

虽然不能从XAML设置此属性，但可以在样式中根据其值创建触发器。

## FrameworkElement.MaxWidth Property

Gets or sets the maximum width constraint of the element.

获取或设置元素的最大宽度约束。

This is one of three properties on [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement?view=windowsdesktop-6.0) that specify width information. The other two are [MinWidth](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.minwidth?view=windowsdesktop-6.0) and [Width](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.width?view=windowsdesktop-6.0). If there is a conflict between these values, the order of application for actual width determination is first [MinWidth](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.minwidth?view=windowsdesktop-6.0) must be honored, then [MaxWidth](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.maxwidth?view=windowsdesktop-6.0), and finally if each of these are within bounds, [Width](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.width?view=windowsdesktop-6.0).

这是FrameworkElement上指定宽度信息的三个属性之一。另外两个是MinWidth和Width。如果这些值之间存在冲突，则实际宽度确定的应用顺序是：首先必须遵循MinWidth，然后是MaxWidth，最后如果每个值都在范围内，则遵循width。

The value restrictions on the [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double?view=windowsdesktop-6.0) value are enforced by a [ValidateValueCallback](https://docs.microsoft.com/en-us/dotnet/api/system.windows.validatevaluecallback?view=windowsdesktop-6.0) mechanism. If you attempt to set an invalid value, a run-time exception is thrown.

对双精度值的值限制由ValidateValueCallback机制强制执行。如果试图设置无效值，将引发运行时异常。

##  FrameworkElement.MaxHeight Property

Gets or sets the maximum height constraint of the element.

获取或设置元素的最大高度约束。

This is one of three properties on [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement?view=windowsdesktop-6.0) that specify height information. The other two are [MinHeight](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.minheight?view=windowsdesktop-6.0) and [Height](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.height?view=windowsdesktop-6.0). If there is a conflict between these values, the order of application for actual height determination is first [MinHeight](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.minheight?view=windowsdesktop-6.0) must be honored, then [MaxHeight](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.maxheight?view=windowsdesktop-6.0), and finally if each of these are within bounds, [Height](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.height?view=windowsdesktop-6.0).

这是FrameworkElement上指定高度信息的三个属性之一。另外两个是MinHeight和Height。如果这些值之间存在冲突，则实际高度确定的应用顺序是：首先必须遵守MinHeight，然后是MaxHeight，最后如果每个值都在范围内，则遵守height。

The value restrictions on the [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double?view=windowsdesktop-6.0) value are enforced by a [ValidateValueCallback](https://docs.microsoft.com/en-us/dotnet/api/system.windows.validatevaluecallback?view=windowsdesktop-6.0) mechanism. If you attempt to set an invalid value a run-time exception is thrown.

对双精度值的值限制由ValidateValueCallback机制强制执行。如果试图设置无效值，将引发运行时异常。

##  FrameworkElement.MinWidth Property

Gets or sets the minimum width constraint of the element.

获取或设置元素的最小宽度约束。

This is one of three properties on [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement?view=windowsdesktop-6.0) that specify width information. The other two are [Width](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.width?view=windowsdesktop-6.0) and [MaxWidth](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.maxwidth?view=windowsdesktop-6.0). If there is a conflict between these values, the order of application for actual width determination is first [MinWidth](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.minwidth?view=windowsdesktop-6.0) must be honored, then [MaxWidth](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.maxwidth?view=windowsdesktop-6.0), and finally if each of these are within bounds, [Width](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.width?view=windowsdesktop-6.0).

这是FrameworkElement上指定宽度信息的三个属性之一。另外两个是Width和MaxWidth。如果这些值之间存在冲突，则实际宽度确定的应用顺序是：首先必须遵循MinWidth，然后是MaxWidth，最后如果每个值都在范围内，则遵循width。

The value restrictions on the [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double?view=windowsdesktop-6.0) value are enforced by a [ValidateValueCallback](https://docs.microsoft.com/en-us/dotnet/api/system.windows.validatevaluecallback?view=windowsdesktop-6.0) mechanism. If you attempt to set an invalid value, a run-time exception is thrown.

对双精度值的值限制由ValidateValueCallback机制强制执行。如果试图设置无效值，将引发运行时异常。

##  FrameworkElement.MinHeight Property

Gets or sets the minimum height constraint of the element.

获取或设置元素的最小高度约束。

This is one of three properties on [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement?view=windowsdesktop-6.0) that specify height information. The other two are [Height](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.height?view=windowsdesktop-6.0) and [MaxHeight](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.maxheight?view=windowsdesktop-6.0). If there is a conflict between these values, the order of application for actual height determination is first [MinHeight](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.minheight?view=windowsdesktop-6.0) must be honored, then [MaxHeight](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.maxheight?view=windowsdesktop-6.0), and finally if each of these are within bounds, [Height](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.height?view=windowsdesktop-6.0).

这是FrameworkElement上指定高度信息的三个属性之一。另外两个是Height和MaxHeight。如果这些值之间存在冲突，则实际高度确定的应用顺序是：首先必须遵守MinHeight，然后是MaxHeight，最后如果每个值都在范围内，则遵守height。

The value restrictions on the [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double?view=windowsdesktop-6.0) value are enforced by a [ValidateValueCallback](https://docs.microsoft.com/en-us/dotnet/api/system.windows.validatevaluecallback?view=windowsdesktop-6.0) mechanism. If you attempt to set an invalid value, a run-time exception is thrown.

 对双精度值的值限制由ValidateValueCallback机制强制执行。如果试图设置无效值，将引发运行时异常。

# 面板

## 自定义 Panel 元素

尽管 WPF 提供了一系列灵活的布局控件，但通过重写 [ArrangeOverride](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.frameworkelement.arrangeoverride) 和 [MeasureOverride](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.frameworkelement.measureoverride) 方法也可以实现自定义布局行为。 可以通过在这些重写方法内定义新的位置行为来实现自定义大小调整和位置。

同样，可以通过重写其 [ArrangeOverride](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.frameworkelement.arrangeoverride) 和 [MeasureOverride](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.frameworkelement.measureoverride) 方法定义基于派生类（如 [Canvas](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.canvas) 或 [Grid](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.grid)）的自定义布局行为。

# 控件

## 控件创作模型

通过丰富内容模型、样式、模板和触发器，最大程度地减少创建新控件的需要。 但是，如果确实需要创建新控件，则理解 WPF 中的不同控件创作模型就显得非常重要。 WPF 提供三个用于创建控件的常规模型，每个模型都提供不同的功能集和灵活度。 三个模型的基类是 [UserControl](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.usercontrol)、[Control](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.control) 和 [FrameworkElement](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.frameworkelement)。

### 从 UserControl 派生

在 WPF 中创建控件的最简单方法是从 [UserControl](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.usercontrol) 派生。 生成继承自 [UserControl](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.usercontrol) 的控件时，会将现有组件添加到 [UserControl](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.usercontrol)，命名这些组件，然后在 WPF 中引用事件处理程序。

如果生成无误，[UserControl](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.usercontrol) 可以利用丰富内容、样式和触发器的优势。 但是，如果控件继承自 [UserControl](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.usercontrol)，则使用该控件的用户将无法使用 [DataTemplate](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.datatemplate) 或 [ControlTemplate](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.controltemplate) 来自定义其外观。 因此，有必要从 [Control](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.control) 类或其派生类（[UserControl](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.usercontrol) 除外）之一中派生，以创建支持模板的自定义控件。

#### 从 UserControl 派生的优点

如果符合以下所有情况，请考虑从 [UserControl](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.usercontrol) 派生：

- 希望采用与生成应用程序相似的方法生成控件。
- 控件仅包含现有组件。
- 无需支持复杂的自定义项。

### 从 Control 派生

从 [Control](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.control) 类派生是大多数现有 WPF 控件使用的模型。 创建从 [Control](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.control) 类派生的控件时，可使用模板定义它的外观。 通过这种方式，可以将运算逻辑从视觉表示形式中分离出来。 通过使用命令和绑定（而不是事件）并尽可能避免引用 [ControlTemplate](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.controltemplate) 中的元素，也可确保分离 UI 和逻辑。 如果控件的 UI 和逻辑正确分离，该控件的用户即可重新定义控件的 [ControlTemplate](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.controltemplate)，从而自定义其外观。 尽管生成自定义 [Control](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.control) 不像生成 [UserControl](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.usercontrol) 那样简单，但自定义 [Control](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.control) 还是提供了最大的灵活性。

#### 从 Control 派生的优点

如果符合以下任一情况，请考虑从 [Control](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.control) 派生，而不要使用 [UserControl](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.usercontrol) 类：

- 希望控件的外观能通过 [ControlTemplate](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.controltemplate) 进行自定义。
- 希望控件支持不同的主题。

### 从 FrameworkElement 派生

从 [UserControl](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.usercontrol) 或 [Control](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.control) 派生的控件依赖于组合现有元素。 在很多情况下，这是一种可接受的解决方案，因为从 [FrameworkElement](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.frameworkelement) 继承的任何对象都可以位于 [ControlTemplate](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.controltemplate) 中。 但是，某些时候，简单的元素组合不能满足控件的外观需要。 对于这些情况，使组件基于 [FrameworkElement](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.frameworkelement) 才是正确的选择。

生成基于 [FrameworkElement](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.frameworkelement) 的组件有两种标准方法：直接呈现和自定义元素组合。 直接呈现涉及的操作包括：重写 [FrameworkElement](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.frameworkelement) 的 [OnRender](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.uielement.onrender) 方法，并提供显式定义组件视觉对象的 [DrawingContext](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.media.drawingcontext) 操作。 这是由 [Image](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.image) 和 [Border](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.border) 使用的方法。 自定义元素组合涉及的操作包括：使用 [Visual](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.media.visual) 类型的对象组合组件的外观。 有关示例，请参阅[使用 DrawingVisual 对象](https://docs.microsoft.com/zh-cn/dotnet/desktop/wpf/graphics-multimedia/using-drawingvisual-objects?view=netframeworkdesktop-4.8)。 [Track](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.primitives.track) 是 WPF 中使用自定义元素组合的控件示例。 在同一控件中，也可以混合使用直接呈现和自定义元素组合。

#### 从 FrameworkElement 派生的优点

如果符合以下任何情况，请考虑从 [FrameworkElement](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.frameworkelement) 派生：

- 希望对控件的外观进行精确控制，而不仅仅是简单的元素组合提供的效果。
- 希望通过定义自己的呈现逻辑来定义控件的外观。
- 希望以一种 [UserControl](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.usercontrol) 和 [Control](https://docs.microsoft.com/zh-CN/dotnet/api/system.windows.controls.control) 之外的新颖方式组合现有元素。

