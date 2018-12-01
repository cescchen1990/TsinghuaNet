﻿#include "pch.h"

#include "CredentialHelper.h"
#include <winrt/Windows.Security.Credentials.h>

using namespace winrt;
using namespace Windows::Security::Credentials;

namespace winrt::TsinghuaNetHelper::implementation
{
    constexpr wchar_t CredentialResource[] = L"TsinghuaNetUWP";

    hstring CredentialHelper::GetCredential(hstring const& username)
    {
        PasswordVault vault;
        auto all = vault.RetrieveAll();
        for (auto c : all)
        {
            if (c.Resource() == CredentialResource && c.UserName() == username)
            {
                c.RetrievePassword();
                return c.Password();
            }
        }
        return {};
    }

    void CredentialHelper::SaveCredential(hstring const& username, hstring const& password)
    {
        PasswordVault vault;
        vault.Add({ CredentialResource, username, password });
    }

    void CredentialHelper::RemoveCredential(hstring const& username)
    {
        PasswordVault vault;
        auto all = vault.RetrieveAll();
        for (auto c : all)
        {
            if (c.Resource() == CredentialResource && c.UserName() == username)
            {
                vault.Remove(c);
            }
        }
    }
} // namespace winrt::TsinghuaNetHelper::implementation