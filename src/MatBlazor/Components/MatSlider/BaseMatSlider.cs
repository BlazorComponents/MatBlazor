﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace MatBlazor
{
    /// <summary>
    /// Material Design Slider for Blazor. Sliders let users select from a range of values by moving the slider thumb.
    /// </summary>
    /// <typeparam name="TValue">sbyte, byte, short, ushort, int, uint, long, ulong, char, float, double, decimal, decimal?</typeparam>
    public class BaseMatSlider<TValue> : BaseMatInputComponent<TValue>
    {
        protected MatDotNetObjectReference<MatSliderJsHelper> jsHelper;

        public BaseMatSlider()
        {
            jsHelper = new MatDotNetObjectReference<MatSliderJsHelper>(new MatSliderJsHelper());
            jsHelper.Value.OnChangeEvent += Value_OnChangeEvent;
            ValueMin = SwitchT.GetMinimum();
            ValueMax = SwitchT.GetMaximum();
            Step = SwitchT.GetStep();

            ClassMapper
                .Add("mat-slider")
                .Add("mdc-slider")
                .If("mdc-slider--discrete", () => Discrete)
                .If("mdc-slider--display-markers", () => Discrete && Markers);
            CallAfterRender(async () =>
            {
                await JsInvokeAsync<object>("matBlazor.matSlider.init", Ref, jsHelper.Reference);
            });
        }

        private void Value_OnChangeEvent(object sender, decimal e)
        {
            CurrentValue = SwitchT.FromDecimal(e);
        }

        public override void Dispose()
        {
            base.Dispose();
            jsHelper.Dispose();
        }


        [Parameter]
        public TValue ValueMin { get; set; }

        [Parameter]
        public TValue ValueMax { get; set; }

        [Parameter]
        public bool Discrete { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public bool Markers { get; set; }

        [Parameter]
        public bool Pin { get; set; }

        [Parameter]
        public TValue Step { get; set; }

        [Parameter]
        public bool EnableStep { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        public string MarkerStyle
        {
            get
            {
                try
                {
                    decimal min = 0;
                    decimal.TryParse(ValueMin.ToString(), out min);
                    decimal max = 0;
                    decimal.TryParse(ValueMax.ToString(), out max);
                    decimal step = 1;
                    decimal.TryParse(Step.ToString(), out step);
                    return "background: linear-gradient(to right, currentcolor 2px, transparent 0px) 0px center / calc((100% - 2px) / " + ((max - min) / step).ToString() + ") 100% repeat-x;";
                }
                catch
                {
                    return "";
                }
            }
        }
    }
}
