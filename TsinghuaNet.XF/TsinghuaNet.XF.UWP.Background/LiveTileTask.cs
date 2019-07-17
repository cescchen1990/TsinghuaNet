﻿using TsinghuaNet.Models;
using TsinghuaNet.XF.Models;
using TsinghuaNet.XF.UWP.Helpers;
using Windows.ApplicationModel.Background;

namespace TsinghuaNet.XF.UWP.Background
{
    public sealed class LiveTileTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            try
            {
                InternetStatus status = new InternetStatus();
                NetXFSettings settings = new NetXFSettings();
                settings.LoadSettings();
                status.Refresh();
                var helper = ConnectHelper.GetHelper(await status.SuggestAsync());
                if (helper != null)
                {
                    using (helper)
                    {
                        FluxUser user = await helper.GetFluxAsync();
                        NotificationHelper.UpdateTile(user);
                        if (settings.EnableFluxLimit)
                            NotificationHelper.SendWarningToast(user, settings.FluxLimit);
                    }
                }
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}