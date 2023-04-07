using Microsoft.AspNetCore.Components;

namespace Portfolio.Components;

public partial class ProjectHeader
{
    [Parameter] public string imagePath { get; set; }
    [Parameter] public string imageAlt { get; set; }
    [Parameter] public string bgColor { get; set; }
    [Parameter] public string bsColumn { get; set; }
    [Parameter] public string imgClass { get; set; }
} 