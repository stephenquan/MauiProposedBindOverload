// MainPage.xaml.cs

using CommunityToolkit.Maui.Markup;

namespace MauiProposedBindOverload;

public partial class MainPage : ContentPage
{
	public ViewModel VM { get; } = new();

	public MainPage()
	{
		BindingContext = VM;

		InitializeComponent();

		// String path, but no compile time safety.
		CounterBtn.Bind(Button.TextProperty, "NestedObject.NestedCount");

		// Compile time safety, but high barrier of entry for nested handlers.
		CounterBtn.Bind(
			Button.TextProperty,
			getter: static (ViewModel vm) => vm.NestedObject.NestedCount,
			handlers:
			[
				(vm => vm, nameof(ViewModel.NestedObject)),
				(vm => vm.NestedObject, nameof(ViewModel.NestedObject.NestedCount)),
			]
			);

		// Proposed compile time safety, with support for nested properties.
		// See ProposedBindableObjectExtensions.cs for the Bind extension method.
		CounterBtn.Bind(Button.TextProperty,
			BindingBase.Create(static (ViewModel vm) => vm.NestedObject.NestedCount, BindingMode.OneWay));
	}

	void OnCounterClicked(object? sender, EventArgs e)
	{
		VM.NestedObject.NestedCount++;
		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}
