using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Maui.Storage;
using Microsoft.UI.Xaml;
using System.Collections.ObjectModel;
using Windows.Foundation.Metadata;
using Windows.Services.Maps;
using Windows.System;

namespace Session_6_Dennis_Hilfinger;

public partial class ManageCharitiesPage : ContentPage
{
    public ObservableCollection<Charity> Charities { get; set; } = new ObservableCollection<Charity>();
    DispatcherTimer timer = new DispatcherTimer();
	public ManageCharitiesPage()
	{
		InitializeComponent();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += timerTick;
        timer.Start();
        this.BindingContext = this;
        LoadData();
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

    private async void LoadData()
    {
        using(var db = new MarathonDB())
        {
            var fastList = await db.Charities.ToListAsync();
            foreach(var charity in fastList)
            {
                Charities.Add(charity);
            }
        }
    }

    private async void EditCharity(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        Charity charityToEdit = btn.CommandParameter as Charity;
        if (charityToEdit != null)
        {
            ShellNavigationQueryParameters data = new ShellNavigationQueryParameters()
            {
                { "PageType", "Edit" },
                { "CharityToEdit", charityToEdit }
            };
            await Shell.Current.GoToAsync("AddEditCharityPage", data);
        }
    }

    private async void AddNewCharity(object sender, EventArgs e)
    {
        ShellNavigationQueryParameters data = new ShellNavigationQueryParameters()
        {
            { "PageType", "Add" }
        };
        await Shell.Current.GoToAsync("AddEditCharityPage", data);
    }
}