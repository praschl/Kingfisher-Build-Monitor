using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Kingfisher.Provider;

namespace Kingfisher.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase
    {
        private readonly IDevOpsApiProvider _devOpsApiProvider;

        private CancellationTokenSource _tokenSource;

        public ConfigurationViewModel(IDevOpsApiProvider devOpsApiProvider)
        {
            _devOpsApiProvider = devOpsApiProvider;
        }

        public string DevOpsServerUrl { get; set; }

        public bool DevOpsServerUrlIsValid { get; set; }

        public bool IsInUrlTestMode { get; set; }

        public bool AutostartEnabled { get; set; }
        public bool AutostartHidden { get; set; }

        public List<TimeSpan> PossibleRefreshTimes { get; set; } = new List<TimeSpan>
        {
            TimeSpan.FromSeconds(5),
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(15),
            TimeSpan.FromSeconds(30),
            TimeSpan.FromMinutes(1),
            TimeSpan.FromMinutes(2),
            TimeSpan.FromMinutes(5),
            TimeSpan.FromMinutes(10),
            TimeSpan.FromMinutes(15)
        };

        public TimeSpan SelectedRefreshTime { get; set; } = TimeSpan.FromSeconds(15);

        public List<TimeSpan> PossibleBuildAge { get; set; } = new List<TimeSpan>
        {
            TimeSpan.FromDays(1),
            TimeSpan.FromDays(2),
            TimeSpan.FromDays(3),
            TimeSpan.FromDays(7),
            TimeSpan.FromDays(14),
            TimeSpan.FromDays(31),
            TimeSpan.FromDays(92),
            TimeSpan.FromDays(366),
        };

        public TimeSpan SelectedBuildAge { get; set; } = TimeSpan.FromDays(3);


        public virtual async void OnDevOpsServerUrlChanged()
        {
            var uri = DevOpsServerUrl;

            try
            {
                _tokenSource?.Cancel();

                IsInUrlTestMode = true;
                DevOpsServerUrlIsValid = false;

                _tokenSource = new CancellationTokenSource();

                var api = _devOpsApiProvider.GetDevOpsApi(uri);

                using (var response = await api.IsOnline(_tokenSource.Token))
                {
                    DevOpsServerUrlIsValid = response.IsSuccessStatusCode;
                    IsInUrlTestMode = false;
                }
            }
            catch (TaskCanceledException)
            {
                // in this case, do not set testmode to false, because the other Task will take care of that
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message); // we dont need full stack trace here
                DevOpsServerUrlIsValid = false;
                IsInUrlTestMode = false;
            }
            finally
            {
                _devOpsApiProvider.Remove(uri);
            }
        }

        public class Designer : ConfigurationViewModel
        {
            public Designer()
                : base(null)
            {
                DevOpsServerUrl = "http://my-tfs-url:8080/tfs/DefaultCollection";
                DevOpsServerUrlIsValid = true;
            }

            // Using a lambda-get this instance won't be created at runtime.
            public static Designer Instance => new Designer();

            public override void OnDevOpsServerUrlChanged()
            {
            }
        }
    }
}