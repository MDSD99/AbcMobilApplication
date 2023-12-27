using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace AbcMobil.CustomControl
{
    public partial class MEntryControl : ContentView
    {
        #region BindableProperty
        public static readonly BindableProperty MTextProperty =
           BindableProperty.Create(
                nameof(MText)
               , typeof(string)
               , typeof(MEntryControl)
               , defaultValue: string.Empty
               , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MTitleProperty =
           BindableProperty.Create(
                nameof(MTitle)
               , typeof(string)
               , typeof(MEntryControl)
               , defaultValue: string.Empty
               , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MTextColorProperty =
             BindableProperty.Create(
                  nameof(MTextColor)
                 , typeof(Color)
                 , typeof(MEntryControl)
                 , defaultValue: Color.AliceBlue
                 , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MTitleColorProperty =
             BindableProperty.Create(
                  nameof(MTitleColor)
                 , typeof(Color)
                 , typeof(MEntryControl)
                 , defaultValue: Color.AliceBlue
                 , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MPlaceHolderProperty =
             BindableProperty.Create(
                  nameof(MPlaceHolder)
                 , typeof(string)
                 , typeof(MEntryControl)
                 , defaultValue: string.Empty
                 , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MPlaceHolderColorProperty =
             BindableProperty.Create(
                  nameof(MPlaceHolderColor)
                 , typeof(Color)
                 , typeof(MEntryControl)
                 , defaultValue: Color.Gray
                 , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MKeyboardProperty =
             BindableProperty.Create(
                  nameof(MKeyboard)
                 , typeof(Keyboard)
                 , typeof(MEntryControl)
                 , defaultValue: Keyboard.Text
                 , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MMaxLenghtProperty =
             BindableProperty.Create(
                  nameof(MMaxLenght)
                 , typeof(int)
                 , typeof(MEntryControl)
                 , defaultValue: 100
                 , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MIsPasswordProperty =
             BindableProperty.Create(
                  nameof(MIsPassword)
                 , typeof(bool)
                 , typeof(MEntryControl)
                 , defaultValue: false
                 , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MIsEnableProperty =
             BindableProperty.Create(
                  nameof(MIsEnable)
                 , typeof(bool)
                 , typeof(MEntryControl)
                 , defaultValue: true
                 , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MMarginnProperty =
             BindableProperty.Create(
                  nameof(MMargin)
                 , typeof(Thickness)
                 , typeof(MEntryControl)
                 , defaultValue: new Thickness(0, 0, 0, 0)
                 , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MBorderColorProperty =
             BindableProperty.Create(
                  nameof(MBorderColor)
                 , typeof(Color)
                 , typeof(MEntryControl)
                 , defaultValue: Color.AliceBlue
                 , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MBackColorProperty =
             BindableProperty.Create(
                  nameof(MBackColor)
                 , typeof(Color)
                 , typeof(MEntryControl)
                 , defaultValue: Color.White
                 , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MFocuesedColorProperty =
             BindableProperty.Create(
                  nameof(MFocusedColor)
                 , typeof(Color)
                 , typeof(MEntryControl)
                 , defaultValue: Color.Red
                 , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MFontSizeProperty =
             BindableProperty.Create(
                  nameof(MFontSize)
                 , typeof(double)
                 , typeof(MEntryControl)
                 , defaultValue: 15.0
                 , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MTitleFontSizeProperty =
             BindableProperty.Create(
                  nameof(MTitleFontSize)
                 , typeof(double)
                 , typeof(MEntryControl)
                 , defaultValue: 15.0
                 , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MTitleFullProperty =
             BindableProperty.Create(
                  nameof(MTitleFull)
                 , typeof(bool)
                 , typeof(MEntryControl)
                 , defaultValue: false
                 , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MCornerRadiusProperty =
            BindableProperty.Create(
                 nameof(MCornerRadius)
                , typeof(int)
                , typeof(MEntryControl)
                , defaultValue: 10
                , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MIsImageProperty =
           BindableProperty.Create(
                nameof(MIsImage)
               , typeof(bool)
               , typeof(MEntryControl)
               , defaultValue: false
               , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MImageProperty =
          BindableProperty.Create(
               nameof(MImage)
              , typeof(string)
              , typeof(MEntryControl)
              , defaultValue: string.Empty
              , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MCommandProperty =
          BindableProperty.Create(
               nameof(MCommand)
              , typeof(ICommand)
              , typeof(MEntryControl)
              , defaultValue: null
              , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MCommandParameterProperty =
          BindableProperty.Create(
               nameof(MCommandParameter)
              , typeof(object)
              , typeof(MEntryControl)
              , defaultValue: null
              , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MWidthProperty =
          BindableProperty.Create(
               nameof(MWidth)
              , typeof(double)
              , typeof(MEntryControl)
              , defaultValue: 10.0
              , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MHeightProperty =
          BindableProperty.Create(
               nameof(MHeight)
              , typeof(double)
              , typeof(MEntryControl)
              , defaultValue: 10.0
              , defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MFocusProperty =
          BindableProperty.Create(
               nameof(MFocus)
              , typeof(bool)
              , typeof(MEntryControl)
              , defaultValue: false
              , defaultBindingMode: BindingMode.TwoWay);
        #endregion
        #region Public Variable
        public string MText
        {
            get => (string)GetValue(MTextProperty);
            set
            {
                SetValue(MTextProperty, value);
            }
        }
        public string MTitle
        {
            get => (string)GetValue(MTitleProperty);
            set
            {
                SetValue(MTitleProperty, value);
            }
        }
        public Color MTextColor
        {
            get => (Color)GetValue(MTextColorProperty);
            set
            {
                SetValue(MTextColorProperty, value);
            }
        }
        public Color MTitleColor
        {
            get => (Color)GetValue(MTitleColorProperty);
            set
            {
                SetValue(MTitleColorProperty, value);
            }
        }
        public string MPlaceHolder
        {
            get => (string)GetValue(MPlaceHolderProperty);
            set
            {
                SetValue(MPlaceHolderProperty, value);
            }
        }
        public Color MPlaceHolderColor
        {
            get => (Color)GetValue(MPlaceHolderColorProperty);
            set
            {
                SetValue(MPlaceHolderColorProperty, value);
            }
        }
        public Keyboard MKeyboard
        {
            get => (Keyboard)GetValue(MKeyboardProperty);
            set
            {
                SetValue(MKeyboardProperty, value);
            }
        }
        public int MMaxLenght
        {
            get => (int)GetValue(MMaxLenghtProperty);
            set
            {
                SetValue(MMaxLenghtProperty, value);
            }
        }
        public bool MIsPassword
        {
            get => (bool)GetValue(MIsPasswordProperty);
            set
            {
                SetValue(MIsPasswordProperty, value);
            }
        }
        public bool MIsEnable
        {
            get => (bool)GetValue(MIsEnableProperty);
            set
            {
                SetValue(MIsEnableProperty, value);
            }
        }
        public Thickness MMargin
        {
            get => (Thickness)GetValue(MMarginnProperty);
            set
            {
                SetValue(MMarginnProperty, value);
            }
        }
        public Color MBorderColor
        {
            get => (Color)GetValue(MBorderColorProperty);
            set
            {
                SetValue(MBorderColorProperty, value);
            }
        }
        public Color MBackColor
        {
            get => (Color)GetValue(MBackColorProperty);
            set
            {
                SetValue(MBackColorProperty, value);
            }
        }
        public Color MFocusedColor
        {
            get => (Color)GetValue(MFocuesedColorProperty);
            set
            {
                SetValue(MFocuesedColorProperty, value);
            }
        }
        public double MFontSize
        {
            get => (double)GetValue(MFontSizeProperty);
            set
            {
                SetValue(MFontSizeProperty, value);
            }
        }
        public double MTitleFontSize
        {
            get => (double)GetValue(MTitleFontSizeProperty);
            set
            {
                SetValue(MTitleFontSizeProperty, value);
            }
        }
        public bool MTitleFull
        {
            get => (bool)GetValue(MTitleFullProperty);
            set
            {
                SetValue(MTitleFullProperty, value);
            }
        }
        public int MCornerRadius
        {
            get => (int)GetValue(MCornerRadiusProperty);
            set
            {
                SetValue(MCornerRadiusProperty, value);
            }
        }
        public bool MIsImage
        {
            get => (bool)GetValue(MIsImageProperty);
            set
            {
                SetValue(MIsImageProperty, value);
            }
        }
        public string MImage
        {
            get => (string)GetValue(MImageProperty);
            set
            {
                SetValue(MImageProperty, value);
            }
        }
        public ICommand MCommand
        {
            get => (ICommand)GetValue(MCommandProperty);
            set
            {
                SetValue(MCommandProperty, value);
            }
        }
        public object MCommandParameter
        {
            get => (object)GetValue(MCommandParameterProperty);
            set
            {
                SetValue(MCommandParameterProperty, value);
            }
        }
        public double MWidth
        {
            get => (double)GetValue(MWidthProperty);
            set
            {
                SetValue(MWidthProperty, value);
            }
        }
        public double MHeight
        {
            get => (double)GetValue(MHeightProperty);
            set
            {
                SetValue(MHeightProperty, value);
            }
        }
        public bool MFocus
        {
            get => (bool)GetValue(MFocusProperty);
            set
            {
                SetValue(MFocusProperty, value);
            }
        }
        #endregion
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName)
            {
                case nameof(MText):
                    entry.Text = MText;
                    break;
                case nameof(MTitle):
                    label.Text = MTitle;
                    break;
                case nameof(MTextColor):
                    entry.TextColor = MTextColor;
                    break;
                case nameof(MTitleColor):
                    label.TextColor = MTitleColor;
                    break;
                case nameof(MPlaceHolder):
                    entry.Placeholder = MPlaceHolder;
                    break;
                case nameof(MPlaceHolderColor):
                    entry.PlaceholderColor = MPlaceHolderColor;
                    break;
                case nameof(MKeyboard):
                    entry.Keyboard = MKeyboard;
                    break;
                case nameof(MMaxLenght):
                    entry.MaxLength = MMaxLenght;
                    break;
                case nameof(MIsPassword):
                    entry.IsPassword = MIsPassword;
                    break;
                case nameof(MIsEnable):
                    entry.IsEnabled = MIsEnable;
                    break;
                case nameof(MMargin):
                    layout.Margin = MMargin;
                    break;
                case nameof(MBorderColor):
                    frame.BorderColor = MBorderColor;
                    break;
                case nameof(MBackColor):
                    frame.BackgroundColor = MBackColor;
                    break;
                case nameof(MFocusedColor):

                    break;
                case nameof(MFontSize):
                    entry.FontSize = MFontSize;
                    break;
                case nameof(MTitleFontSize):
                    label.FontSize = MTitleFontSize;
                    break;
                case nameof(MTitleFull):
                    label.IsVisible = MTitleFull;
                    break;
                case nameof(MCornerRadius):
                    frame.CornerRadius = MCornerRadius;
                    break;
                case nameof(MIsImage):
                    img.IsEnabled = MIsImage;
                    img.IsVisible = MIsImage;
                    break;
                case nameof(MImage):
                    img.Source = ImageSource.FromResource(MImage);
                    break;
                case nameof(MCommand):
                    img.Command = MCommand;
                    break;
                case nameof(MCommandParameter):
                    img.CommandParameter = MCommandParameter;
                    break;
                case nameof(MWidth):
                    img.WidthRequest = MWidth;
                    break;
                case nameof(MHeight):
                    img.HeightRequest = MHeight;
                    break;
                case nameof(MFocus):
                    if (MFocus)
                        entry.Focus();
                    else
                        entry.Unfocus();
                    break;
                default:
                    break;
            }
        }
        public MEntryControl()
        {
            InitializeComponent();
            entry.TextChanged += (sender, e) => { 
                MText = e.NewTextValue; 
            };
        }
        private void entry_Focused(object sender, FocusEventArgs e)
        {
            frame.BorderColor = MFocusedColor;
            label.TextColor = MFocusedColor;
        }
        private void entry_Unfocused(object sender, FocusEventArgs e)
        {
            frame.BorderColor = MBorderColor;
            label.TextColor = MTitleColor;
        }
    }
}