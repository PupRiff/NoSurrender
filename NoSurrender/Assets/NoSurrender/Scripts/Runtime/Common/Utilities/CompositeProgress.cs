using System;
using System.Collections.Generic;
using NoSurrender.Common.Exceptions;

namespace NoSurrender.Common.Utilities
{
    /// <summary>
    /// 複数の進捗を組み合わせて1つの進捗に変換する
    /// </summary>
    public sealed class CompositeProgress
    {
        private readonly IProgress<float> _combinedProgress;
        private readonly List<ProgressInfo> _progressInfos = new();
        private readonly object _lock = new();
        private int _index;
        
        public CompositeProgress(IProgress<float> combinedProgress)
        {
            Throw.IfArgumentNull(combinedProgress);
            
            _combinedProgress = combinedProgress;
        }
        
        public IProgress<float> CreateSubProgress(float weight = 1f)
        {
            if (weight <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(weight), "重みは正の値である必要があります");
            }

            int index;

            lock (_lock)
            {
                index = _index++;
                _progressInfos.Add(new ProgressInfo { Current = 0f, Weight = weight });
            }
            
            return new SubProgress(this, index);
        }

        private void ReportSubProgress(int index, float value)
        {
            Throw.IfArgumentOutOfRange(value, 0f, 1f);
            
            float combinedValue;

            lock (_lock)
            {
                _progressInfos[index] = _progressInfos[index] with { Current = value };

                float totalWeight = 0;
                float weightedSum = 0;

                foreach (var pi in _progressInfos)
                {
                    totalWeight += pi.Weight;
                    weightedSum += pi.Current * pi.Weight;
                }

                combinedValue = totalWeight > 0f ? weightedSum / totalWeight : 0f;
            }

            _combinedProgress.Report(combinedValue);
        }

        private sealed class SubProgress : IProgress<float>
        {
            private readonly CompositeProgress _parent;
            private readonly int _index;

            public SubProgress(CompositeProgress parent, int index)
            {
                _parent = parent;
                _index = index;
            }

            public void Report(float value)
            {
                _parent.ReportSubProgress(_index, value);
            }
        }
        
        private readonly struct ProgressInfo
        {
            public required float Current { get; init; }
            public required float Weight { get; init; }
        }
    }
}