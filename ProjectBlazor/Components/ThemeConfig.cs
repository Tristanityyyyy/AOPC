namespace ProjectBlazor.Components;

public static class ThemeConfig
{
    public static class Colors
    {
        public const string Primary = "#C9A84C";
        public const string PrimaryDark = "#B8942E";
        public const string OnWhite = "white";
        public static class Header
        {
            public const string Start = "#0D1B2A";
            public const string Middle = "#134E4A";
            public const string End = "#0F766E";
            public const string DarkVariant = "#1B2B3A";
            public const string DarkerVariant = "#1A2E3B";
        }

        public static class Text
        {
            public const string OnPrimary = "white";
            public const string Primary = "#0D1B2A";
            public const string Secondary = "#6B7280";
            public const string Muted = "#9CA3AF";
            public const string LightGray = "#D1D5DB";
            public const string CloseButton = "#222222";
            public const string LabelAccent = "#92700E";
        }

        public static class Border
        {
            public const string Default = "#E5E7EB";
            public const string Focus = "#C9A84C";
            public const string Light = "#E5E7EB";
            public const string Subtle = "#F0F0F0";
        }

        public static class Background
        {
            public const string Card = "white";
            public const string Alt = "#F3F4F6";
            public const string Hover = "#FEF3C7";
            public const string Subtle = "#FAFAFA";
            public const string Warning = "#FEF3C7";
            public const string WarningLight = "#FFFBEB";
            public const string WarningBorder = "#FCD34D";
        }
    }

    public static class Styles
    {
        public static string ButtonPrimary => $"background: linear-gradient(135deg,{Colors.Primary},{Colors.PrimaryDark}); color:{Colors.Text.OnPrimary};";
        public static string ButtonPrimarySolid => $"background:{Colors.Primary}; color:{Colors.Text.OnPrimary};";
        public static string HeaderGradient => $"background: linear-gradient(135deg,{Colors.Header.Start} 0%,{Colors.Header.Middle} 90%,{Colors.Header.End} 100%);";
        public static string TextPrimary => $"color:{Colors.Text.Primary};";
        public static string TextAccent => $"color:{Colors.Primary};";
        public static string BorderFocus => $"border-color:{Colors.Border.Focus};";
    }
}