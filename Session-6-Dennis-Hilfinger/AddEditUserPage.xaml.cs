using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Printing;
using Windows.System;
using Windows.UI.WebUI;

namespace Session_6_Dennis_Hilfinger;

public partial class AddEditUserPage : ContentPage
{
    bool IsEditPage = false;
    DispatcherTimer timer = new DispatcherTimer();
    User? userToEdit;

    public AddEditUserPage()
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
        userToEdit = (User) query["UserToEdit"];
        
        if (IsEditPage && userToEdit != null)
        {

            FillData();
        } else
        {
            Entry emailEntry = new Entry();
            DataLayout.Children.Add(emailEntry);
        }
    }

    private void FillData()
    {
        using (var db = new MarathonDB())
        {
            
        }
    }

    private void SaveData(object sender, EventArgs e)
    {/*
        using (var db = new MarathonDB())
        {
            if (!String.IsNullOrEmpty(FirstnameEntry.Text))
            {
                user.FirstName = FirstnameEntry.Text.ToString();
            }
            if (!String.IsNullOrEmpty(LastnameEntry.Text))
            {
                user.LastName = LastnameEntry.Text.ToString();
            }

            var genders = db.Genders.ToList();
            var userGender = genders.FirstOrDefault(g => g.Gender1 == GenderPicker.SelectedItem.ToString());
            var countries = db.Countries.ToList();
            var userCountry = countries.FirstOrDefault(c => c.CountryName + " - " + c.CountryCode == CountryPicker.SelectedItem.ToString());

            if (CheckBirthdate())
            {
                user.Runners.First().DateOfBirth = BirthdatePicker.Date;
            }
            else
            {
                return;
            }
            user.Runners.First().GenderNavigation = userGender;
            user.Runners.First().Gender = userGender.Gender1;
            user.Runners.First().CountryCodeNavigation = userCountry;
            user.Runners.First().CountryCode = userCountry.CountryCode;

            if (!String.IsNullOrEmpty(PasswordEntry.Text) ||
                !String.IsNullOrEmpty(PasswordAgainEntry.Text))
            {
                if (PasswordAgainEntry.Text.ToString() == PasswordEntry.Text.ToString())
                {
                    if (CheckPasswordRequirements())
                    {
                        user.Password = PasswordEntry.Text.ToString();
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    DisplayAlert("Error", "Passwords do not match.", "OK");
                    return;
                }
            }
            else if (!(String.IsNullOrEmpty(PasswordEntry.Text) && String.IsNullOrEmpty(PasswordAgainEntry.Text)))
            {
                DisplayAlert("Error", "Please enter a value for both password fields to change your password.", "OK");
                return;
            }
            db.Update(user);

            var regEvent = db.Runners
                .Include(r => r.Registrations)
                .FirstOrDefault(r => r.Email == user.Email)
                .Registrations.First();
            var newStatus = db.RegistrationStatuses.FirstOrDefault(st => st.RegistrationStatus1 == RegStatusPicker.SelectedItem.ToString());
            regEvent.RegistrationStatus = newStatus;
            db.Update(regEvent);

            db.SaveChanges();
            DisplayAlert("Success", "Profile updated successfully.", "OK");
        }*/
    }

    private bool CheckPasswordRequirements()
    {
        string password = PasswordEntry.Text.ToString();
        if (password.Length < 6)
        {
            DisplayAlert("Error", "Password must be at least 6 characters long.", "OK");
            return false;
        }
        if (!password.Any(char.IsUpper))
        {
            DisplayAlert("Error", "Password must contain at least one uppercase letter.", "OK");
            return false;
        }
        if (!password.Any(char.IsLower))
        {
            DisplayAlert("Error", "Password must contain at least one lowercase letter.", "OK");
            return false;
        }
        if (!password.Any(char.IsDigit))
        {
            DisplayAlert("Error", "Password must contain at least one digit.", "OK");
            return false;
        }
        var specialCharacters = "!@#$%^";
        if (!password.Any(ch => specialCharacters.Contains(ch)))
        {
            DisplayAlert("Error", "Password must contain at least one special character.", "OK");
            return false;
        }
        return true;
    }

    private void Cancel(object sender, EventArgs e)
    {
        Navigation.RemovePage(this);
    }
}