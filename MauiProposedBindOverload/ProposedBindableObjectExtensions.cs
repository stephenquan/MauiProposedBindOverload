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
}
