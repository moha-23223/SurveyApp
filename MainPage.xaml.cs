namespace SurveyApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    // The result button works only while the switch is on
    private void OnAgreeToggled(object sender, ToggledEventArgs e)
    {
        ShowButton.IsEnabled = e.Value;
    }

    private async void OnShowClicked(object sender, EventArgs e)
    {
        // Required fields
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            await DisplayAlert("Ошибка", "Введите имя", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(ContactEntry.Text))
        {
            await DisplayAlert("Ошибка", "Введите телефон", "OK");
            return;
        }

        if (DevicePicker.SelectedIndex == -1)
        {
            await DisplayAlert("Ошибка", "Выберите устройство", "OK");
            return;
        }

        // Which radio button is selected
        RadioButton selectedPlan = Plan50Radio;
        if (Plan100Radio.IsChecked)
            selectedPlan = Plan100Radio;
        else if (PlanUnlimRadio.IsChecked)
            selectedPlan = PlanUnlimRadio;

        // Checked protocols
        var protocols = new List<string>();
        if (AwgCheck.IsChecked)
            protocols.Add("AmneziaWG");
        if (VlessCheck.IsChecked)
            protocols.Add("VLESS");
        string protocolText = protocols.Count > 0
            ? string.Join(", ", protocols)
            : "не выбраны";

        string comment = string.IsNullOrWhiteSpace(CommentEditor.Text)
            ? "нет"
            : CommentEditor.Text;

        ResultLabel.Text =
            $"Имя: {NameEntry.Text}\n" +
            $"Телефон: {ContactEntry.Text}\n" +
            $"Устройство: {DevicePicker.SelectedItem}\n" +
            $"Тариф: {selectedPlan.Value}\n" +
            $"Протоколы: {protocolText}\n" +
            $"Дата начала: {StartDatePicker.Date:dd.MM.yyyy}\n" +
            $"Количество устройств: {DevicesStepper.Value:F0}\n" +
            $"Комментарий: {comment}\n" +
            $"Согласие с правилами: {(AgreeSwitch.IsToggled ? "да" : "нет")}";

        ResultLabel.IsVisible = true;
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        NameEntry.Text = string.Empty;
        ContactEntry.Text = string.Empty;
        CommentEditor.Text = string.Empty;
        DevicePicker.SelectedIndex = -1;
        Plan50Radio.IsChecked = true;
        AwgCheck.IsChecked = true;
        VlessCheck.IsChecked = false;
        StartDatePicker.Date = new DateTime(2026, 10, 9);
        DevicesStepper.Value = 1;
        AgreeSwitch.IsToggled = false;

        ResultLabel.Text = string.Empty;
        ResultLabel.IsVisible = false;
    }
}