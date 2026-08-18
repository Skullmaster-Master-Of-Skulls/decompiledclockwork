using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace NLog
{
	// Token: 0x0200006A RID: 106
	[CLSCompliant(false)]
	public interface ILoggerBase
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000275 RID: 629
		// (remove) Token: 0x06000276 RID: 630
		event EventHandler<EventArgs> LoggerReconfigured;

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000277 RID: 631
		string Name { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000278 RID: 632
		LogFactory Factory { get; }

		// Token: 0x06000279 RID: 633
		bool IsEnabled(LogLevel level);

		// Token: 0x0600027A RID: 634
		void Log(LogEventInfo logEvent);

		// Token: 0x0600027B RID: 635
		void Log(Type wrapperType, LogEventInfo logEvent);

		// Token: 0x0600027C RID: 636
		void Log<T>(LogLevel level, T value);

		// Token: 0x0600027D RID: 637
		void Log<T>(LogLevel level, IFormatProvider formatProvider, T value);

		// Token: 0x0600027E RID: 638
		void Log(LogLevel level, LogMessageGenerator messageFunc);

		// Token: 0x0600027F RID: 639
		[Obsolete("Use Log(LogLevel level, Exception exception, [Localizable(false)] string message, params object[] args)")]
		void LogException(LogLevel level, [Localizable(false)] string message, Exception exception);

		// Token: 0x06000280 RID: 640
		[StringFormatMethod("message")]
		void Log(LogLevel level, Exception exception, [Localizable(false)] string message, params object[] args);

		// Token: 0x06000281 RID: 641
		[StringFormatMethod("message")]
		void Log(LogLevel level, Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x06000282 RID: 642
		[StringFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x06000283 RID: 643
		void Log(LogLevel level, [Localizable(false)] string message);

		// Token: 0x06000284 RID: 644
		[StringFormatMethod("message")]
		void Log(LogLevel level, [Localizable(false)] string message, params object[] args);

		// Token: 0x06000285 RID: 645
		[Obsolete("Use Log(LogLevel level, Exception exception, [Localizable(false)] string message, params object[] args)")]
		void Log(LogLevel level, [Localizable(false)] string message, Exception exception);

		// Token: 0x06000286 RID: 646
		[StringFormatMethod("message")]
		void Log<TArgument>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

		// Token: 0x06000287 RID: 647
		[StringFormatMethod("message")]
		void Log<TArgument>(LogLevel level, [Localizable(false)] string message, TArgument argument);

		// Token: 0x06000288 RID: 648
		[StringFormatMethod("message")]
		void Log<TArgument1, TArgument2>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x06000289 RID: 649
		[StringFormatMethod("message")]
		void Log<TArgument1, TArgument2>(LogLevel level, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x0600028A RID: 650
		[StringFormatMethod("message")]
		void Log<TArgument1, TArgument2, TArgument3>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x0600028B RID: 651
		[StringFormatMethod("message")]
		void Log<TArgument1, TArgument2, TArgument3>(LogLevel level, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x0600028C RID: 652
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Log(LogLevel level, object value);

		// Token: 0x0600028D RID: 653
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Log(LogLevel level, IFormatProvider formatProvider, object value);

		// Token: 0x0600028E RID: 654
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Log(LogLevel level, string message, object arg1, object arg2);

		// Token: 0x0600028F RID: 655
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Log(LogLevel level, string message, object arg1, object arg2, object arg3);

		// Token: 0x06000290 RID: 656
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, bool argument);

		// Token: 0x06000291 RID: 657
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Log(LogLevel level, string message, bool argument);

		// Token: 0x06000292 RID: 658
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, char argument);

		// Token: 0x06000293 RID: 659
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Log(LogLevel level, string message, char argument);

		// Token: 0x06000294 RID: 660
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, byte argument);

		// Token: 0x06000295 RID: 661
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Log(LogLevel level, string message, byte argument);

		// Token: 0x06000296 RID: 662
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, string argument);

		// Token: 0x06000297 RID: 663
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Log(LogLevel level, string message, string argument);

		// Token: 0x06000298 RID: 664
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, int argument);

		// Token: 0x06000299 RID: 665
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Log(LogLevel level, string message, int argument);

		// Token: 0x0600029A RID: 666
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, long argument);

		// Token: 0x0600029B RID: 667
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Log(LogLevel level, string message, long argument);

		// Token: 0x0600029C RID: 668
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, float argument);

		// Token: 0x0600029D RID: 669
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Log(LogLevel level, string message, float argument);

		// Token: 0x0600029E RID: 670
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, double argument);

		// Token: 0x0600029F RID: 671
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Log(LogLevel level, string message, double argument);

		// Token: 0x060002A0 RID: 672
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, decimal argument);

		// Token: 0x060002A1 RID: 673
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Log(LogLevel level, string message, decimal argument);

		// Token: 0x060002A2 RID: 674
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, object argument);

		// Token: 0x060002A3 RID: 675
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Log(LogLevel level, string message, object argument);

		// Token: 0x060002A4 RID: 676
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, sbyte argument);

		// Token: 0x060002A5 RID: 677
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Log(LogLevel level, string message, sbyte argument);

		// Token: 0x060002A6 RID: 678
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, uint argument);

		// Token: 0x060002A7 RID: 679
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Log(LogLevel level, string message, uint argument);

		// Token: 0x060002A8 RID: 680
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, ulong argument);

		// Token: 0x060002A9 RID: 681
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Log(LogLevel level, string message, ulong argument);
	}
}
