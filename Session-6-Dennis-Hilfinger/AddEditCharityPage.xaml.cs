using Microsoft.Maui.Storage;
using Microsoft.UI.Xaml;

namespace Session_6_Dennis_Hilfinger;

public partial class AddEditCharityPage : ContentPage, IQueryAttributable
{
    bool IsEditPage = false;
    DispatcherTimer timer = new DispatcherTimer();
    Charity? charityToEdit;
    public AddEditCharityPage()
    {
        InitializeComponent();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += timerTick;
        timer.Start();
    }

    private void timerTick(object? sender, object e)
    {
        DateTime targetTime = new DateTime(2026, 9, 5, 6, 0, 0);
        DateTime currentTime = DateTime.Now;
        TimeSpan timeDiff = targetTime - currentTime;

        TimerLabel.Text = string.Format("{0} days {1} hours and {2} minutes until the race starts!",
            timeDiff.Days,
            timeDiff.Hours,
            timeDiff.Minutes);
    }
    private void Logout(object? sender, EventArgs e)
    {
        AppShell.Current.GoToAsync("//MainPage");
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        IsEditPage = (query["PageType"].ToString() == "Edit");
        query.TryGetValue("CharityToEdit", out object charityObj);
        if (charityObj != null)
        {
            charityToEdit = (Charity)charityObj;
        }

        if (IsEditPage && charityToEdit != null)
        {
            HeadingLabel.Text = "Edit charity";
        }
        else
        {
            HeadingLabel.Text = "Add charity";
        }
        FillData();
    }

    private void FillData()
    {
        using (var db = new MarathonDB())
        {
            if (IsEditPage)
            {
                NameEntry.Text = charityToEdit.CharityName;
                DescriptionEntry.Text = charityToEdit.CharityDescription;
            }
        }
    }

    private async void SaveData(object sender, EventArgs e)
    {
        await DisplayAlert("Info", "Feature not implemented yet", "Ok");
        using (var db = new MarathonDB())
        {
            if (IsEditPage)
            {
                /*Üvar charity = db.Charities.FirstOrDefault();
                

                db.Update(charity);
                db.SaveChanges();
                await DisplayAlert("Success", "Charity updated successfully.", "OK");*/
            }
            else
            {
                /*
                var charity = new Charity();

                db.Charities.Add(charity);
                db.SaveChanges();
                await DisplayAlert("Success", "Charity created successfully.", "OK");
                Cancel(null, EventArgs.Empty);*/
            }
        }
    }

    private void Cancel(object sender, EventArgs e)
    {
        Navigation.RemovePage(this);
    }
}