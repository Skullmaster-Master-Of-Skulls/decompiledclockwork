using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

namespace System.Windows.Input
{
	// Token: 0x020003A2 RID: 930
	[TypeForwardedFrom("PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[TypeConverter("System.Windows.Input.CommandConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
	[ValueSerializer("System.Windows.Input.CommandValueSerializer, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
	[__DynamicallyInvokable]
	public interface ICommand
	{
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060022AE RID: 8878
		// (remove) Token: 0x060022AF RID: 8879
		[__DynamicallyInvokable]
		event EventHandler CanExecuteChanged;

		// Token: 0x060022B0 RID: 8880
		[__DynamicallyInvokable]
		bool CanExecute(object parameter);

		// Token: 0x060022B1 RID: 8881
		[__DynamicallyInvokable]
		void Execute(object parameter);
	}
}
