using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace NLog
{
	// Token: 0x0200006C RID: 108
	[CLSCompliant(false)]
	public interface ILogger : ILoggerBase, ISuppress
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002B2 RID: 690
		bool IsTraceEnabled { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002B3 RID: 691
		bool IsDebugEnabled { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002B4 RID: 692
		bool IsInfoEnabled { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002B5 RID: 693
		bool IsWarnEnabled { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002B6 RID: 694
		bool IsErrorEnabled { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002B7 RID: 695
		bool IsFatalEnabled { get; }

		// Token: 0x060002B8 RID: 696
		void Trace<T>(T value);

		// Token: 0x060002B9 RID: 697
		void Trace<T>(IFormatProvider formatProvider, T value);

		// Token: 0x060002BA RID: 698
		void Trace(LogMessageGenerator messageFunc);

		// Token: 0x060002BB RID: 699
		[Obsolete("Use Trace(Exception exception, string message, params object[] args) method instead.")]
		void TraceException([Localizable(false)] string message, Exception exception);

		// Token: 0x060002BC RID: 700
		[StringFormatMethod("message")]
		void Trace(Exception exception, [Localizable(false)] string message, params object[] args);

		// Token: 0x060002BD RID: 701
		[StringFormatMethod("message")]
		void Trace(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x060002BE RID: 702
		[StringFormatMethod("message")]
		void Trace(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x060002BF RID: 703
		void Trace([Localizable(false)] string message);

		// Token: 0x060002C0 RID: 704
		[StringFormatMethod("message")]
		void Trace([Localizable(false)] string message, params object[] args);

		// Token: 0x060002C1 RID: 705
		[Obsolete("Use Trace(Exception exception, string message, params object[] args) method instead.")]
		void Trace([Localizable(false)] string message, Exception exception);

		// Token: 0x060002C2 RID: 706
		[StringFormatMethod("message")]
		void Trace<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

		// Token: 0x060002C3 RID: 707
		[StringFormatMethod("message")]
		void Trace<TArgument>([Localizable(false)] string message, TArgument argument);

		// Token: 0x060002C4 RID: 708
		[StringFormatMethod("message")]
		void Trace<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x060002C5 RID: 709
		[StringFormatMethod("message")]
		void Trace<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x060002C6 RID: 710
		[StringFormatMethod("message")]
		void Trace<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x060002C7 RID: 711
		[StringFormatMethod("message")]
		void Trace<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x060002C8 RID: 712
		void Debug<T>(T value);

		// Token: 0x060002C9 RID: 713
		void Debug<T>(IFormatProvider formatProvider, T value);

		// Token: 0x060002CA RID: 714
		void Debug(LogMessageGenerator messageFunc);

		// Token: 0x060002CB RID: 715
		[Obsolete("Use Debug(Exception exception, string message, params object[] args) method instead.")]
		void DebugException([Localizable(false)] string message, Exception exception);

		// Token: 0x060002CC RID: 716
		[StringFormatMethod("message")]
		void Debug(Exception exception, [Localizable(false)] string message, params object[] args);

		// Token: 0x060002CD RID: 717
		[StringFormatMethod("message")]
		void Debug(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x060002CE RID: 718
		[StringFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x060002CF RID: 719
		void Debug([Localizable(false)] string message);

		// Token: 0x060002D0 RID: 720
		[StringFormatMethod("message")]
		void Debug([Localizable(false)] string message, params object[] args);

		// Token: 0x060002D1 RID: 721
		[Obsolete("Use Debug(Exception exception, string message, params object[] args) method instead.")]
		void Debug([Localizable(false)] string message, Exception exception);

		// Token: 0x060002D2 RID: 722
		[StringFormatMethod("message")]
		void Debug<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

		// Token: 0x060002D3 RID: 723
		[StringFormatMethod("message")]
		void Debug<TArgument>([Localizable(false)] string message, TArgument argument);

		// Token: 0x060002D4 RID: 724
		[StringFormatMethod("message")]
		void Debug<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x060002D5 RID: 725
		[StringFormatMethod("message")]
		void Debug<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x060002D6 RID: 726
		[StringFormatMethod("message")]
		void Debug<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x060002D7 RID: 727
		[StringFormatMethod("message")]
		void Debug<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x060002D8 RID: 728
		void Info<T>(T value);

		// Token: 0x060002D9 RID: 729
		void Info<T>(IFormatProvider formatProvider, T value);

		// Token: 0x060002DA RID: 730
		void Info(LogMessageGenerator messageFunc);

		// Token: 0x060002DB RID: 731
		[Obsolete("Use Info(Exception exception, string message, params object[] args) method instead.")]
		void InfoException([Localizable(false)] string message, Exception exception);

		// Token: 0x060002DC RID: 732
		[StringFormatMethod("message")]
		void Info(Exception exception, [Localizable(false)] string message, params object[] args);

		// Token: 0x060002DD RID: 733
		[StringFormatMethod("message")]
		void Info(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x060002DE RID: 734
		[StringFormatMethod("message")]
		void Info(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x060002DF RID: 735
		void Info([Localizable(false)] string message);

		// Token: 0x060002E0 RID: 736
		[StringFormatMethod("message")]
		void Info([Localizable(false)] string message, params object[] args);

		// Token: 0x060002E1 RID: 737
		[Obsolete("Use Info(Exception exception, string message, params object[] args) method instead.")]
		void Info([Localizable(false)] string message, Exception exception);

		// Token: 0x060002E2 RID: 738
		[StringFormatMethod("message")]
		void Info<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

		// Token: 0x060002E3 RID: 739
		[StringFormatMethod("message")]
		void Info<TArgument>([Localizable(false)] string message, TArgument argument);

		// Token: 0x060002E4 RID: 740
		[StringFormatMethod("message")]
		void Info<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x060002E5 RID: 741
		[StringFormatMethod("message")]
		void Info<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x060002E6 RID: 742
		[StringFormatMethod("message")]
		void Info<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x060002E7 RID: 743
		[StringFormatMethod("message")]
		void Info<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x060002E8 RID: 744
		void Warn<T>(T value);

		// Token: 0x060002E9 RID: 745
		void Warn<T>(IFormatProvider formatProvider, T value);

		// Token: 0x060002EA RID: 746
		void Warn(LogMessageGenerator messageFunc);

		// Token: 0x060002EB RID: 747
		[Obsolete("Use Warn(Exception exception, string message, params object[] args) method instead.")]
		void WarnException([Localizable(false)] string message, Exception exception);

		// Token: 0x060002EC RID: 748
		[StringFormatMethod("message")]
		void Warn(Exception exception, [Localizable(false)] string message, params object[] args);

		// Token: 0x060002ED RID: 749
		[StringFormatMethod("message")]
		void Warn(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x060002EE RID: 750
		[StringFormatMethod("message")]
		void Warn(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x060002EF RID: 751
		void Warn([Localizable(false)] string message);

		// Token: 0x060002F0 RID: 752
		[StringFormatMethod("message")]
		void Warn([Localizable(false)] string message, params object[] args);

		// Token: 0x060002F1 RID: 753
		[Obsolete("Use Warn(Exception exception, string message, params object[] args) method instead.")]
		void Warn([Localizable(false)] string message, Exception exception);

		// Token: 0x060002F2 RID: 754
		[StringFormatMethod("message")]
		void Warn<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

		// Token: 0x060002F3 RID: 755
		[StringFormatMethod("message")]
		void Warn<TArgument>([Localizable(false)] string message, TArgument argument);

		// Token: 0x060002F4 RID: 756
		[StringFormatMethod("message")]
		void Warn<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x060002F5 RID: 757
		[StringFormatMethod("message")]
		void Warn<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x060002F6 RID: 758
		[StringFormatMethod("message")]
		void Warn<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x060002F7 RID: 759
		[StringFormatMethod("message")]
		void Warn<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x060002F8 RID: 760
		void Error<T>(T value);

		// Token: 0x060002F9 RID: 761
		void Error<T>(IFormatProvider formatProvider, T value);

		// Token: 0x060002FA RID: 762
		void Error(LogMessageGenerator messageFunc);

		// Token: 0x060002FB RID: 763
		[Obsolete("Use Error(Exception exception, string message, params object[] args) method instead.")]
		void ErrorException([Localizable(false)] string message, Exception exception);

		// Token: 0x060002FC RID: 764
		[StringFormatMethod("message")]
		void Error(Exception exception, [Localizable(false)] string message, params object[] args);

		// Token: 0x060002FD RID: 765
		[StringFormatMethod("message")]
		void Error(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x060002FE RID: 766
		[StringFormatMethod("message")]
		void Error(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x060002FF RID: 767
		void Error([Localizable(false)] string message);

		// Token: 0x06000300 RID: 768
		[StringFormatMethod("message")]
		void Error([Localizable(false)] string message, params object[] args);

		// Token: 0x06000301 RID: 769
		[Obsolete("Use Error(Exception exception, string message, params object[] args) method instead.")]
		void Error([Localizable(false)] string message, Exception exception);

		// Token: 0x06000302 RID: 770
		[StringFormatMethod("message")]
		void Error<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

		// Token: 0x06000303 RID: 771
		[StringFormatMethod("message")]
		void Error<TArgument>([Localizable(false)] string message, TArgument argument);

		// Token: 0x06000304 RID: 772
		[StringFormatMethod("message")]
		void Error<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x06000305 RID: 773
		[StringFormatMethod("message")]
		void Error<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x06000306 RID: 774
		[StringFormatMethod("message")]
		void Error<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x06000307 RID: 775
		[StringFormatMethod("message")]
		void Error<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x06000308 RID: 776
		void Fatal<T>(T value);

		// Token: 0x06000309 RID: 777
		void Fatal<T>(IFormatProvider formatProvider, T value);

		// Token: 0x0600030A RID: 778
		void Fatal(LogMessageGenerator messageFunc);

		// Token: 0x0600030B RID: 779
		[Obsolete("Use Fatal(Exception exception, string message, params object[] args) method instead.")]
		void FatalException([Localizable(false)] string message, Exception exception);

		// Token: 0x0600030C RID: 780
		[StringFormatMethod("message")]
		void Fatal(Exception exception, [Localizable(false)] string message, params object[] args);

		// Token: 0x0600030D RID: 781
		[StringFormatMethod("message")]
		void Fatal(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x0600030E RID: 782
		[StringFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x0600030F RID: 783
		void Fatal([Localizable(false)] string message);

		// Token: 0x06000310 RID: 784
		[StringFormatMethod("message")]
		void Fatal([Localizable(false)] string message, params object[] args);

		// Token: 0x06000311 RID: 785
		[Obsolete("Use Fatal(Exception exception, string message, params object[] args) method instead.")]
		void Fatal([Localizable(false)] string message, Exception exception);

		// Token: 0x06000312 RID: 786
		[StringFormatMethod("message")]
		void Fatal<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

		// Token: 0x06000313 RID: 787
		[StringFormatMethod("message")]
		void Fatal<TArgument>([Localizable(false)] string message, TArgument argument);

		// Token: 0x06000314 RID: 788
		[StringFormatMethod("message")]
		void Fatal<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x06000315 RID: 789
		[StringFormatMethod("message")]
		void Fatal<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x06000316 RID: 790
		[StringFormatMethod("message")]
		void Fatal<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x06000317 RID: 791
		[StringFormatMethod("message")]
		void Fatal<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x06000318 RID: 792
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(object value);

		// Token: 0x06000319 RID: 793
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(IFormatProvider formatProvider, object value);

		// Token: 0x0600031A RID: 794
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(string message, object arg1, object arg2);

		// Token: 0x0600031B RID: 795
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Trace(string message, object arg1, object arg2, object arg3);

		// Token: 0x0600031C RID: 796
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(IFormatProvider formatProvider, string message, bool argument);

		// Token: 0x0600031D RID: 797
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(string message, bool argument);

		// Token: 0x0600031E RID: 798
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(IFormatProvider formatProvider, string message, char argument);

		// Token: 0x0600031F RID: 799
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Trace(string message, char argument);

		// Token: 0x06000320 RID: 800
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Trace(IFormatProvider formatProvider, string message, byte argument);

		// Token: 0x06000321 RID: 801
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Trace(string message, byte argument);

		// Token: 0x06000322 RID: 802
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Trace(IFormatProvider formatProvider, string message, string argument);

		// Token: 0x06000323 RID: 803
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Trace(string message, string argument);

		// Token: 0x06000324 RID: 804
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(IFormatProvider formatProvider, string message, int argument);

		// Token: 0x06000325 RID: 805
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Trace(string message, int argument);

		// Token: 0x06000326 RID: 806
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(IFormatProvider formatProvider, string message, long argument);

		// Token: 0x06000327 RID: 807
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Trace(string message, long argument);

		// Token: 0x06000328 RID: 808
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(IFormatProvider formatProvider, string message, float argument);

		// Token: 0x06000329 RID: 809
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(string message, float argument);

		// Token: 0x0600032A RID: 810
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(IFormatProvider formatProvider, string message, double argument);

		// Token: 0x0600032B RID: 811
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(string message, double argument);

		// Token: 0x0600032C RID: 812
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(IFormatProvider formatProvider, string message, decimal argument);

		// Token: 0x0600032D RID: 813
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Trace(string message, decimal argument);

		// Token: 0x0600032E RID: 814
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Trace(IFormatProvider formatProvider, string message, object argument);

		// Token: 0x0600032F RID: 815
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(string message, object argument);

		// Token: 0x06000330 RID: 816
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(IFormatProvider formatProvider, string message, sbyte argument);

		// Token: 0x06000331 RID: 817
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(string message, sbyte argument);

		// Token: 0x06000332 RID: 818
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(IFormatProvider formatProvider, string message, uint argument);

		// Token: 0x06000333 RID: 819
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(string message, uint argument);

		// Token: 0x06000334 RID: 820
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(IFormatProvider formatProvider, string message, ulong argument);

		// Token: 0x06000335 RID: 821
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Trace(string message, ulong argument);

		// Token: 0x06000336 RID: 822
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(object value);

		// Token: 0x06000337 RID: 823
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(IFormatProvider formatProvider, object value);

		// Token: 0x06000338 RID: 824
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(string message, object arg1, object arg2);

		// Token: 0x06000339 RID: 825
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(string message, object arg1, object arg2, object arg3);

		// Token: 0x0600033A RID: 826
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, string message, bool argument);

		// Token: 0x0600033B RID: 827
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Debug(string message, bool argument);

		// Token: 0x0600033C RID: 828
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(IFormatProvider formatProvider, string message, char argument);

		// Token: 0x0600033D RID: 829
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(string message, char argument);

		// Token: 0x0600033E RID: 830
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(IFormatProvider formatProvider, string message, byte argument);

		// Token: 0x0600033F RID: 831
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(string message, byte argument);

		// Token: 0x06000340 RID: 832
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, string message, string argument);

		// Token: 0x06000341 RID: 833
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(string message, string argument);

		// Token: 0x06000342 RID: 834
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, string message, int argument);

		// Token: 0x06000343 RID: 835
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Debug(string message, int argument);

		// Token: 0x06000344 RID: 836
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, string message, long argument);

		// Token: 0x06000345 RID: 837
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(string message, long argument);

		// Token: 0x06000346 RID: 838
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(IFormatProvider formatProvider, string message, float argument);

		// Token: 0x06000347 RID: 839
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(string message, float argument);

		// Token: 0x06000348 RID: 840
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(IFormatProvider formatProvider, string message, double argument);

		// Token: 0x06000349 RID: 841
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(string message, double argument);

		// Token: 0x0600034A RID: 842
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(IFormatProvider formatProvider, string message, decimal argument);

		// Token: 0x0600034B RID: 843
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(string message, decimal argument);

		// Token: 0x0600034C RID: 844
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(IFormatProvider formatProvider, string message, object argument);

		// Token: 0x0600034D RID: 845
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Debug(string message, object argument);

		// Token: 0x0600034E RID: 846
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, string message, sbyte argument);

		// Token: 0x0600034F RID: 847
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(string message, sbyte argument);

		// Token: 0x06000350 RID: 848
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(IFormatProvider formatProvider, string message, uint argument);

		// Token: 0x06000351 RID: 849
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(string message, uint argument);

		// Token: 0x06000352 RID: 850
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(IFormatProvider formatProvider, string message, ulong argument);

		// Token: 0x06000353 RID: 851
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(string message, ulong argument);

		// Token: 0x06000354 RID: 852
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(object value);

		// Token: 0x06000355 RID: 853
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(IFormatProvider formatProvider, object value);

		// Token: 0x06000356 RID: 854
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Info(string message, object arg1, object arg2);

		// Token: 0x06000357 RID: 855
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Info(string message, object arg1, object arg2, object arg3);

		// Token: 0x06000358 RID: 856
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(IFormatProvider formatProvider, string message, bool argument);

		// Token: 0x06000359 RID: 857
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(string message, bool argument);

		// Token: 0x0600035A RID: 858
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(IFormatProvider formatProvider, string message, char argument);

		// Token: 0x0600035B RID: 859
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(string message, char argument);

		// Token: 0x0600035C RID: 860
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(IFormatProvider formatProvider, string message, byte argument);

		// Token: 0x0600035D RID: 861
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(string message, byte argument);

		// Token: 0x0600035E RID: 862
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(IFormatProvider formatProvider, string message, string argument);

		// Token: 0x0600035F RID: 863
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Info(string message, string argument);

		// Token: 0x06000360 RID: 864
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(IFormatProvider formatProvider, string message, int argument);

		// Token: 0x06000361 RID: 865
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Info(string message, int argument);

		// Token: 0x06000362 RID: 866
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(IFormatProvider formatProvider, string message, long argument);

		// Token: 0x06000363 RID: 867
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(string message, long argument);

		// Token: 0x06000364 RID: 868
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(IFormatProvider formatProvider, string message, float argument);

		// Token: 0x06000365 RID: 869
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(string message, float argument);

		// Token: 0x06000366 RID: 870
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(IFormatProvider formatProvider, string message, double argument);

		// Token: 0x06000367 RID: 871
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Info(string message, double argument);

		// Token: 0x06000368 RID: 872
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(IFormatProvider formatProvider, string message, decimal argument);

		// Token: 0x06000369 RID: 873
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(string message, decimal argument);

		// Token: 0x0600036A RID: 874
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(IFormatProvider formatProvider, string message, object argument);

		// Token: 0x0600036B RID: 875
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(string message, object argument);

		// Token: 0x0600036C RID: 876
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Info(IFormatProvider formatProvider, string message, sbyte argument);

		// Token: 0x0600036D RID: 877
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Info(string message, sbyte argument);

		// Token: 0x0600036E RID: 878
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(IFormatProvider formatProvider, string message, uint argument);

		// Token: 0x0600036F RID: 879
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Info(string message, uint argument);

		// Token: 0x06000370 RID: 880
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(IFormatProvider formatProvider, string message, ulong argument);

		// Token: 0x06000371 RID: 881
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Info(string message, ulong argument);

		// Token: 0x06000372 RID: 882
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(object value);

		// Token: 0x06000373 RID: 883
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(IFormatProvider formatProvider, object value);

		// Token: 0x06000374 RID: 884
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(string message, object arg1, object arg2);

		// Token: 0x06000375 RID: 885
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Warn(string message, object arg1, object arg2, object arg3);

		// Token: 0x06000376 RID: 886
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Warn(IFormatProvider formatProvider, string message, bool argument);

		// Token: 0x06000377 RID: 887
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Warn(string message, bool argument);

		// Token: 0x06000378 RID: 888
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(IFormatProvider formatProvider, string message, char argument);

		// Token: 0x06000379 RID: 889
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Warn(string message, char argument);

		// Token: 0x0600037A RID: 890
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(IFormatProvider formatProvider, string message, byte argument);

		// Token: 0x0600037B RID: 891
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Warn(string message, byte argument);

		// Token: 0x0600037C RID: 892
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(IFormatProvider formatProvider, string message, string argument);

		// Token: 0x0600037D RID: 893
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(string message, string argument);

		// Token: 0x0600037E RID: 894
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(IFormatProvider formatProvider, string message, int argument);

		// Token: 0x0600037F RID: 895
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Warn(string message, int argument);

		// Token: 0x06000380 RID: 896
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(IFormatProvider formatProvider, string message, long argument);

		// Token: 0x06000381 RID: 897
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(string message, long argument);

		// Token: 0x06000382 RID: 898
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Warn(IFormatProvider formatProvider, string message, float argument);

		// Token: 0x06000383 RID: 899
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Warn(string message, float argument);

		// Token: 0x06000384 RID: 900
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(IFormatProvider formatProvider, string message, double argument);

		// Token: 0x06000385 RID: 901
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(string message, double argument);

		// Token: 0x06000386 RID: 902
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(IFormatProvider formatProvider, string message, decimal argument);

		// Token: 0x06000387 RID: 903
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(string message, decimal argument);

		// Token: 0x06000388 RID: 904
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(IFormatProvider formatProvider, string message, object argument);

		// Token: 0x06000389 RID: 905
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(string message, object argument);

		// Token: 0x0600038A RID: 906
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Warn(IFormatProvider formatProvider, string message, sbyte argument);

		// Token: 0x0600038B RID: 907
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(string message, sbyte argument);

		// Token: 0x0600038C RID: 908
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(IFormatProvider formatProvider, string message, uint argument);

		// Token: 0x0600038D RID: 909
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(string message, uint argument);

		// Token: 0x0600038E RID: 910
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Warn(IFormatProvider formatProvider, string message, ulong argument);

		// Token: 0x0600038F RID: 911
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Warn(string message, ulong argument);

		// Token: 0x06000390 RID: 912
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(object value);

		// Token: 0x06000391 RID: 913
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(IFormatProvider formatProvider, object value);

		// Token: 0x06000392 RID: 914
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Error(string message, object arg1, object arg2);

		// Token: 0x06000393 RID: 915
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(string message, object arg1, object arg2, object arg3);

		// Token: 0x06000394 RID: 916
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Error(IFormatProvider formatProvider, string message, bool argument);

		// Token: 0x06000395 RID: 917
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(string message, bool argument);

		// Token: 0x06000396 RID: 918
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(IFormatProvider formatProvider, string message, char argument);

		// Token: 0x06000397 RID: 919
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Error(string message, char argument);

		// Token: 0x06000398 RID: 920
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Error(IFormatProvider formatProvider, string message, byte argument);

		// Token: 0x06000399 RID: 921
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(string message, byte argument);

		// Token: 0x0600039A RID: 922
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(IFormatProvider formatProvider, string message, string argument);

		// Token: 0x0600039B RID: 923
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Error(string message, string argument);

		// Token: 0x0600039C RID: 924
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(IFormatProvider formatProvider, string message, int argument);

		// Token: 0x0600039D RID: 925
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(string message, int argument);

		// Token: 0x0600039E RID: 926
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(IFormatProvider formatProvider, string message, long argument);

		// Token: 0x0600039F RID: 927
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(string message, long argument);

		// Token: 0x060003A0 RID: 928
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(IFormatProvider formatProvider, string message, float argument);

		// Token: 0x060003A1 RID: 929
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(string message, float argument);

		// Token: 0x060003A2 RID: 930
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(IFormatProvider formatProvider, string message, double argument);

		// Token: 0x060003A3 RID: 931
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(string message, double argument);

		// Token: 0x060003A4 RID: 932
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Error(IFormatProvider formatProvider, string message, decimal argument);

		// Token: 0x060003A5 RID: 933
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Error(string message, decimal argument);

		// Token: 0x060003A6 RID: 934
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(IFormatProvider formatProvider, string message, object argument);

		// Token: 0x060003A7 RID: 935
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(string message, object argument);

		// Token: 0x060003A8 RID: 936
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Error(IFormatProvider formatProvider, string message, sbyte argument);

		// Token: 0x060003A9 RID: 937
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(string message, sbyte argument);

		// Token: 0x060003AA RID: 938
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(IFormatProvider formatProvider, string message, uint argument);

		// Token: 0x060003AB RID: 939
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(string message, uint argument);

		// Token: 0x060003AC RID: 940
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Error(IFormatProvider formatProvider, string message, ulong argument);

		// Token: 0x060003AD RID: 941
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(string message, ulong argument);

		// Token: 0x060003AE RID: 942
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Fatal(object value);

		// Token: 0x060003AF RID: 943
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Fatal(IFormatProvider formatProvider, object value);

		// Token: 0x060003B0 RID: 944
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Fatal(string message, object arg1, object arg2);

		// Token: 0x060003B1 RID: 945
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Fatal(string message, object arg1, object arg2, object arg3);

		// Token: 0x060003B2 RID: 946
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, bool argument);

		// Token: 0x060003B3 RID: 947
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Fatal(string message, bool argument);

		// Token: 0x060003B4 RID: 948
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, char argument);

		// Token: 0x060003B5 RID: 949
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Fatal(string message, char argument);

		// Token: 0x060003B6 RID: 950
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, byte argument);

		// Token: 0x060003B7 RID: 951
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Fatal(string message, byte argument);

		// Token: 0x060003B8 RID: 952
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, string argument);

		// Token: 0x060003B9 RID: 953
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Fatal(string message, string argument);

		// Token: 0x060003BA RID: 954
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Fatal(IFormatProvider formatProvider, string message, int argument);

		// Token: 0x060003BB RID: 955
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Fatal(string message, int argument);

		// Token: 0x060003BC RID: 956
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, long argument);

		// Token: 0x060003BD RID: 957
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Fatal(string message, long argument);

		// Token: 0x060003BE RID: 958
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Fatal(IFormatProvider formatProvider, string message, float argument);

		// Token: 0x060003BF RID: 959
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Fatal(string message, float argument);

		// Token: 0x060003C0 RID: 960
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, double argument);

		// Token: 0x060003C1 RID: 961
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Fatal(string message, double argument);

		// Token: 0x060003C2 RID: 962
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Fatal(IFormatProvider formatProvider, string message, decimal argument);

		// Token: 0x060003C3 RID: 963
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Fatal(string message, decimal argument);

		// Token: 0x060003C4 RID: 964
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, object argument);

		// Token: 0x060003C5 RID: 965
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Fatal(string message, object argument);

		// Token: 0x060003C6 RID: 966
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, sbyte argument);

		// Token: 0x060003C7 RID: 967
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Fatal(string message, sbyte argument);

		// Token: 0x060003C8 RID: 968
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, uint argument);

		// Token: 0x060003C9 RID: 969
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Fatal(string message, uint argument);

		// Token: 0x060003CA RID: 970
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Fatal(IFormatProvider formatProvider, string message, ulong argument);

		// Token: 0x060003CB RID: 971
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		void Fatal(string message, ulong argument);
	}
}
