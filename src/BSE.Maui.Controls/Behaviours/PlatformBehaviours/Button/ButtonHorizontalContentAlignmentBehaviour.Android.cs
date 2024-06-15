using Google.Android.Material.Button;

namespace BSE.Maui.Controls.Behaviours
{
    public partial class HorizontalContentAlignmentBehaviour
    {
        protected override void OnAttachedTo(Button bindable, Android.Views.View platformView)
        {
            ApplyHorizontalContentAlignment(platformView, HorizontalContentAlignment);
            base.OnAttachedTo(bindable, platformView);
        }

        protected override void OnDetachedFrom(Button bindable, Android.Views.View platformView)
        {
            ClearHorizontalContentAlignment(platformView);
            base.OnDetachedFrom(bindable, platformView);
        }

        void ApplyHorizontalContentAlignment(Android.Views.View platformView, TextAlignment textAlignment = TextAlignment.Center)
        {
            if (platformView is MaterialButton button)
            {
                switch (textAlignment)
                {
                    case TextAlignment.Start:
                        button.TextAlignment = Android.Views.TextAlignment.TextStart;
                        break;
                    case TextAlignment.End:
                        button.TextAlignment = Android.Views.TextAlignment.TextEnd;
                        break;
                }
            }
        }

        void ClearHorizontalContentAlignment(Android.Views.View platformView)
        {
            if (platformView is MaterialButton button)
            {
                button.TextAlignment = Android.Views.TextAlignment.Center;
            }
        }
    }
}
