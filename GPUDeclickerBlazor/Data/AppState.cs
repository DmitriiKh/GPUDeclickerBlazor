﻿using AudioClickRepair.Data;
using System;

namespace GPUDeclickerBlazor.Data
{
    public class AppState
    {
        const double _defaultThreshold = 10f;
        const int _defaultMaxLength = 250;

        public string OutputFileNameSuggestion { get; set; } = String.Empty;

        // Audio processing parameters
        public double Threshold
        {
            get
            {
                return Audio is null 
                    ? _defaultThreshold 
                    : Audio.Settings.ThresholdForDetection;
            }
            set
            {
                if (Audio != null)
                    Audio.Settings.ThresholdForDetection = value;
            }
        }

        public int MaxLength
        {
            get
            {
                return Audio is null
                    ? _defaultMaxLength
                    : Audio.Settings.MaxLengthOfCorrection;
            }
            set
            {
                if (Audio != null)
                    Audio.Settings.MaxLengthOfCorrection = value;
            }
        }

        public IAudio Audio { get; private set; }

        public event Action OnAudioChange;

        public void SetAudioData(IAudio audioData)
        {
            if (audioData is null)
                return;

            Audio = audioData;
            Audio.Settings.ThresholdForDetection = _defaultThreshold;
            Audio.Settings.MaxLengthOfCorrection = _defaultMaxLength;
            NotifyAudioDataChanged();
        }

        private void NotifyAudioDataChanged() => OnAudioChange?.Invoke();

        public Patch[] GetAudioClicks(ChannelType channelType)
        {
            if (Audio is null)
                return null;

            return Audio.GetPatches(channelType);
        }
    }
}
