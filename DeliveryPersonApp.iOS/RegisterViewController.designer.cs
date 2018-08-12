// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    [Register ("RegisterViewController")]
    partial class RegisterViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ConfirmPasswordTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField PasswordTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton RegisterSaveButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField UsenameTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ConfirmPasswordTextField != null) {
                ConfirmPasswordTextField.Dispose ();
                ConfirmPasswordTextField = null;
            }

            if (PasswordTextField != null) {
                PasswordTextField.Dispose ();
                PasswordTextField = null;
            }

            if (RegisterSaveButton != null) {
                RegisterSaveButton.Dispose ();
                RegisterSaveButton = null;
            }

            if (UsenameTextField != null) {
                UsenameTextField.Dispose ();
                UsenameTextField = null;
            }
        }
    }
}