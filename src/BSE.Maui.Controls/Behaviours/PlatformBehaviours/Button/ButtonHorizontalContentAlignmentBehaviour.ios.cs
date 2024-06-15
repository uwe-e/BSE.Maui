using UIKit;

namespace BSE.Maui.Controls.Behaviours
{
    public partial class HorizontalContentAlignmentBehaviour
    {
        protected override void OnAttachedTo(Button bindable, UIView platformView)
        {
            ApplyHorizontalContentAlignment(platformView, HorizontalContentAlignment);
            base.OnAttachedTo(bindable, platformView);
        }

        protected override void OnDetachedFrom(Button bindable, UIView platformView)
        {
            ClearHorizontalContentAlignment(platformView);
            base.OnDetachedFrom(bindable, platformView);
        }

        void ApplyHorizontalContentAlignment(UIView platformView, TextAlignment textAlignment = TextAlignment.Center)
        {
            if (platformView is UIButton button)
            {
                switch (textAlignment)
                {
                    case TextAlignment.Start:
                        button.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
                        break;
                    case TextAlignment.End:
                        button.HorizontalAlignment = UIControlContentHorizontalAlignment.Right;
                        break;
                }
            }
        }

        void ClearHorizontalContentAlignment(UIView platformView)
        {
            if (platformView is UIButton button)
            {
                button.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            }
        }
    }
}
