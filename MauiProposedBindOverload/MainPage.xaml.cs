// MainPage.xaml.cs

using CommunityToolkit.Maui.Markup;

namespace MauiProposedBindOverload;

public partial class MainPage : ContentPage
{
	int count = 0;

	public ViewModel VM { get; } = new ViewModel { NestedObject = { Text = "Hello World" } };

	public MainPage()
	{
		BindingContext = VM;

		InitializeComponent();

		// String path, but no compile time safety.
		var entry1 = new Entry().Bind(Entry.TextProperty, "NestedObject.Text");

		// Compile time safety, but high barrier of entry for nested handlers.
		var entry2 = new Entry().Bind(
			Entry.TextProperty,
			getter: static (ViewModel vm) => vm.NestedObject.Text,
			handlers:
			[
				(vm => vm, nameof(ViewModel.NestedObject)),
				(vm => vm.NestedObject, nameof(ViewModel.NestedObject.Text)),
			],
			setter: static (ViewModel vm, string? text) => vm.NestedObject.Text = text);

		// Proposed new API overload, with support for nested properties.
		// See ProposedBindableObjectExtensions.cs for the Bind extension method.
		var entry3 = new Entry().Bind(Entry.TextProperty, BindingBase.Create(static (ViewModel vm) => vm.NestedObject.Text));

		// Add the entries to the page for demonstration.
		vsl.Children.Add(new Label { Text = "Entry with string path:" });
		vsl.Children.Add(entry1);
		vsl.Children.Add(new Label { Text = "Entry with compile time safety:" });
		vsl.Children.Add(entry2);
		vsl.Children.Add(new Label { Text = "Entry with proposed API new overload:" });
		vsl.Children.Add(entry3);
	}

	void OnCounterClicked(object? sender, EventArgs e)
	{
		count++;

		CounterBtn.Text = (count == 1)
			? $"Clicked {count} time"
			: $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

