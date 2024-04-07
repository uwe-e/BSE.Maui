namespace BSE.Maui.Controls
{
    public partial class TabbedContainerPage : TabbedPage, IElementConfiguration<TabbedContainerPage>, ITabbedContainerView
    {
        public static readonly BindableProperty BottomViewProperty
            = BindableProperty.Create(nameof(BottomView),
                                      typeof(ContentView),
                                      typeof(TabbedContainerPage),
                                      null,
                                      propertyChanged: OnBottomViewChanged);

        readonly Lazy<PlatformConfigurationRegistry<TabbedContainerPage>> _platformConfigurationRegistry;

        public ContentView BottomView
        {
            get { return (ContentView)GetValue(BottomViewProperty); }
            set { SetValue(BottomViewProperty, value); }
        }

        public IView BottomContent => BottomView;

        public TabbedContainerPage()
        {
            _platformConfigurationRegistry = new Lazy<PlatformConfigurationRegistry<TabbedContainerPage>>(() => new PlatformConfigurationRegistry<TabbedContainerPage>(this));
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            ContentView contentView = BottomView;
            if (contentView != null)
            {
                SetInheritedBindingContext(contentView, BindingContext);
            }
        }

        private static void OnBottomViewChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newElement = (Element)newValue;
            if (newElement != null)
            {
                BindableObject.SetInheritedBindingContext(newElement, bindable.BindingContext);
            }
        }

        IPlatformElementConfiguration<T, TabbedContainerPage> IElementConfiguration<TabbedContainerPage>.On<T>()
        {
            return _platformConfigurationRegistry.Value.On<T>();
        }
        
    }
}
