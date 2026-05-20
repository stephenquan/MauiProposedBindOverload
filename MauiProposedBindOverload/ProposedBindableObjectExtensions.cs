// ProposedBindableObjectExtensions.cs

namespace MauiProposedBindOverload;

static class ProposedBindableObjectExtensions
{
	public static TBindable Bind<TBindable>(
	  this TBindable bindable,
	  BindableProperty targetProperty,
	  BindingBase binding)
	  where TBindable : BindableObject
	{
		bindable.SetBinding(targetProperty, binding);
		return bindable;
	}

	public static TBindable Bind<TBindable>(
		this TBindable bindable,
		BindingBase binding)
		where TBindable : InputView
	{
		return Bind(bindable, InputView.TextProperty, binding);
	}
}
