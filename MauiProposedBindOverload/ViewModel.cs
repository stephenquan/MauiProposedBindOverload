// ViewModel.cs

using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiProposedBindOverload;

public partial class ViewModel
{
	public NestedViewModel NestedObject { get; } = new();
}

public partial class NestedViewModel : ObservableObject
{
	[ObservableProperty] public partial int NestedCount { get; set; } = 0;
}
