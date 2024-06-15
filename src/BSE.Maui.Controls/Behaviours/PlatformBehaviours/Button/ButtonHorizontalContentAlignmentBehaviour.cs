namespace BSE.Maui.Controls.Behaviours
{
    public partial class HorizontalContentAlignmentBehaviour : PlatformBehavior<Button>
    {
        public static readonly BindableProperty HorizontalContentAlignmentProperty =
            BindableProperty.Create(
                nameof(HorizontalContentAlignment),
                typeof(TextAlignment), typeof(HorizontalContentAlignmentBehaviour), TextAlignment.Center);

        public TextAlignment HorizontalContentAlignment
        {
            get => (TextAlignment)GetValue(HorizontalContentAlignmentProperty);
            set => SetValue(HorizontalContentAlignmentProperty, value);
        }
    }
}
