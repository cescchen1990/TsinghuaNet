﻿#pragma once
#include "SsidSuggestion.g.h"

#include "../Shared/Utility.h"

namespace winrt::TsinghuaNetUWP::implementation
{
    struct SsidSuggestion : SsidSuggestionT<SsidSuggestion>
    {
        SsidSuggestion();

        DEPENDENCY_PROPERTY(Ssid, hstring)
        DEPENDENCY_PROPERTY(Value, int)

        DEPENDENCY_PROPERTY(SsidStyle, Windows::UI::Xaml::Style)

    public:
        Windows::Foundation::Collections::IObservableVector<Windows::Foundation::IInspectable> States() { return m_States; }

    private:
        Windows::Foundation::Collections::IObservableVector<Windows::Foundation::IInspectable> m_States;
    };
} // namespace winrt::TsinghuaNetUWP::implementation

namespace winrt::TsinghuaNetUWP::factory_implementation
{
    struct SsidSuggestion : SsidSuggestionT<SsidSuggestion, implementation::SsidSuggestion>
    {
    };
} // namespace winrt::TsinghuaNetUWP::factory_implementation
