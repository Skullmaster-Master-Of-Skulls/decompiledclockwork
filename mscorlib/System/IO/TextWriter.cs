using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.IO
{
	// Token: 0x020005CA RID: 1482
	[ComVisible(true)]
	[Serializable]
	public abstract class TextWriter : MarshalByRefObject, IDisposable
	{
		// Token: 0x0600370C RID: 14092 RVA: 0x000BA4FC File Offset: 0x000B94FC
		protected TextWriter()
		{
			this.InternalFormatProvider = null;
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x000BA530 File Offset: 0x000B9530
		protected TextWriter(IFormatProvider formatProvider)
		{
			this.InternalFormatProvider = formatProvider;
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x0600370E RID: 14094 RVA: 0x000BA562 File Offset: 0x000B9562
		public virtual IFormatProvider FormatProvider
		{
			get
			{
				if (this.InternalFormatProvider == null)
				{
					return Thread.CurrentThread.CurrentCulture;
				}
				return this.InternalFormatProvider;
			}
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x000BA57D File Offset: 0x000B957D
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x000BA58C File Offset: 0x000B958C
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x000BA58E File Offset: 0x000B958E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x000BA59D File Offset: 0x000B959D
		public virtual void Flush()
		{
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06003713 RID: 14099
		public abstract Encoding Encoding { get; }

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06003714 RID: 14100 RVA: 0x000BA59F File Offset: 0x000B959F
		// (set) Token: 0x06003715 RID: 14101 RVA: 0x000BA5AC File Offset: 0x000B95AC
		public virtual string NewLine
		{
			get
			{
				return new string(this.CoreNewLine);
			}
			set
			{
				if (value == null)
				{
					value = "\r\n";
				}
				this.CoreNewLine = value.ToCharArray();
			}
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x000BA5C4 File Offset: 0x000B95C4
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static TextWriter Synchronized(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer is TextWriter.SyncTextWriter)
			{
				return writer;
			}
			return new TextWriter.SyncTextWriter(writer);
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x000BA5E4 File Offset: 0x000B95E4
		public virtual void Write(char value)
		{
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x000BA5E6 File Offset: 0x000B95E6
		public virtual void Write(char[] buffer)
		{
			if (buffer != null)
			{
				this.Write(buffer, 0, buffer.Length);
			}
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x000BA5F8 File Offset: 0x000B95F8
		public virtual void Write(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			for (int i = 0; i < count; i++)
			{
				this.Write(buffer[index + i]);
			}
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x000BA67E File Offset: 0x000B967E
		public virtual void Write(bool value)
		{
			this.Write(value ? "True" : "False");
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x000BA695 File Offset: 0x000B9695
		public virtual void Write(int value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x000BA6AA File Offset: 0x000B96AA
		[CLSCompliant(false)]
		public virtual void Write(uint value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x000BA6BF File Offset: 0x000B96BF
		public virtual void Write(long value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x000BA6D4 File Offset: 0x000B96D4
		[CLSCompliant(false)]
		public virtual void Write(ulong value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x000BA6E9 File Offset: 0x000B96E9
		public virtual void Write(float value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x000BA6FE File Offset: 0x000B96FE
		public virtual void Write(double value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x000BA713 File Offset: 0x000B9713
		public virtual void Write(decimal value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x000BA728 File Offset: 0x000B9728
		public virtual void Write(string value)
		{
			if (value != null)
			{
				this.Write(value.ToCharArray());
			}
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x000BA73C File Offset: 0x000B973C
		public virtual void Write(object value)
		{
			if (value != null)
			{
				IFormattable formattable = value as IFormattable;
				if (formattable != null)
				{
					this.Write(formattable.ToString(null, this.FormatProvider));
					return;
				}
				this.Write(value.ToString());
			}
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x000BA778 File Offset: 0x000B9778
		public virtual void Write(string format, object arg0)
		{
			this.Write(string.Format(this.FormatProvider, format, new object[]
			{
				arg0
			}));
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x000BA7A4 File Offset: 0x000B97A4
		public virtual void Write(string format, object arg0, object arg1)
		{
			this.Write(string.Format(this.FormatProvider, format, new object[]
			{
				arg0,
				arg1
			}));
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x000BA7D4 File Offset: 0x000B97D4
		public virtual void Write(string format, object arg0, object arg1, object arg2)
		{
			this.Write(string.Format(this.FormatProvider, format, new object[]
			{
				arg0,
				arg1,
				arg2
			}));
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x000BA808 File Offset: 0x000B9808
		public virtual void Write(string format, params object[] arg)
		{
			this.Write(string.Format(this.FormatProvider, format, arg));
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x000BA81D File Offset: 0x000B981D
		public virtual void WriteLine()
		{
			this.Write(this.CoreNewLine);
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x000BA82B File Offset: 0x000B982B
		public virtual void WriteLine(char value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x000BA83A File Offset: 0x000B983A
		public virtual void WriteLine(char[] buffer)
		{
			this.Write(buffer);
			this.WriteLine();
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x000BA849 File Offset: 0x000B9849
		public virtual void WriteLine(char[] buffer, int index, int count)
		{
			this.Write(buffer, index, count);
			this.WriteLine();
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000BA85A File Offset: 0x000B985A
		public virtual void WriteLine(bool value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000BA869 File Offset: 0x000B9869
		public virtual void WriteLine(int value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x000BA878 File Offset: 0x000B9878
		[CLSCompliant(false)]
		public virtual void WriteLine(uint value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x000BA887 File Offset: 0x000B9887
		public virtual void WriteLine(long value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x000BA896 File Offset: 0x000B9896
		[CLSCompliant(false)]
		public virtual void WriteLine(ulong value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x000BA8A5 File Offset: 0x000B98A5
		public virtual void WriteLine(float value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x000BA8B4 File Offset: 0x000B98B4
		public virtual void WriteLine(double value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x000BA8C3 File Offset: 0x000B98C3
		public virtual void WriteLine(decimal value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x000BA8D4 File Offset: 0x000B98D4
		public virtual void WriteLine(string value)
		{
			if (value == null)
			{
				this.WriteLine();
				return;
			}
			int length = value.Length;
			int num = this.CoreNewLine.Length;
			char[] array = new char[length + num];
			value.CopyTo(0, array, 0, length);
			if (num == 2)
			{
				array[length] = this.CoreNewLine[0];
				array[length + 1] = this.CoreNewLine[1];
			}
			else if (num == 1)
			{
				array[length] = this.CoreNewLine[0];
			}
			else
			{
				Buffer.InternalBlockCopy(this.CoreNewLine, 0, array, length * 2, num * 2);
			}
			this.Write(array, 0, length + num);
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x000BA95C File Offset: 0x000B995C
		public virtual void WriteLine(object value)
		{
			if (value == null)
			{
				this.WriteLine();
				return;
			}
			IFormattable formattable = value as IFormattable;
			if (formattable != null)
			{
				this.WriteLine(formattable.ToString(null, this.FormatProvider));
				return;
			}
			this.WriteLine(value.ToString());
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x000BA9A0 File Offset: 0x000B99A0
		public virtual void WriteLine(string format, object arg0)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, new object[]
			{
				arg0
			}));
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x000BA9CC File Offset: 0x000B99CC
		public virtual void WriteLine(string format, object arg0, object arg1)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, new object[]
			{
				arg0,
				arg1
			}));
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x000BA9FC File Offset: 0x000B99FC
		public virtual void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, new object[]
			{
				arg0,
				arg1,
				arg2
			}));
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x000BAA30 File Offset: 0x000B9A30
		public virtual void WriteLine(string format, params object[] arg)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg));
		}

		// Token: 0x04001CC2 RID: 7362
		private const string InitialNewLine = "\r\n";

		// Token: 0x04001CC3 RID: 7363
		public static readonly TextWriter Null = new TextWriter.NullTextWriter();

		// Token: 0x04001CC4 RID: 7364
		protected char[] CoreNewLine = new char[]
		{
			'\r',
			'\n'
		};

		// Token: 0x04001CC5 RID: 7365
		private IFormatProvider InternalFormatProvider;

		// Token: 0x020005CB RID: 1483
		[Serializable]
		private sealed class NullTextWriter : TextWriter
		{
			// Token: 0x0600373B RID: 14139 RVA: 0x000BAA51 File Offset: 0x000B9A51
			internal NullTextWriter() : base(CultureInfo.InvariantCulture)
			{
			}

			// Token: 0x17000951 RID: 2385
			// (get) Token: 0x0600373C RID: 14140 RVA: 0x000BAA5E File Offset: 0x000B9A5E
			public override Encoding Encoding
			{
				get
				{
					return Encoding.Default;
				}
			}

			// Token: 0x0600373D RID: 14141 RVA: 0x000BAA65 File Offset: 0x000B9A65
			public override void Write(char[] buffer, int index, int count)
			{
			}

			// Token: 0x0600373E RID: 14142 RVA: 0x000BAA67 File Offset: 0x000B9A67
			public override void Write(string value)
			{
			}

			// Token: 0x0600373F RID: 14143 RVA: 0x000BAA69 File Offset: 0x000B9A69
			public override void WriteLine()
			{
			}

			// Token: 0x06003740 RID: 14144 RVA: 0x000BAA6B File Offset: 0x000B9A6B
			public override void WriteLine(string value)
			{
			}

			// Token: 0x06003741 RID: 14145 RVA: 0x000BAA6D File Offset: 0x000B9A6D
			public override void WriteLine(object value)
			{
			}
		}

		// Token: 0x020005CC RID: 1484
		[Serializable]
		internal sealed class SyncTextWriter : TextWriter, IDisposable
		{
			// Token: 0x06003742 RID: 14146 RVA: 0x000BAA6F File Offset: 0x000B9A6F
			internal SyncTextWriter(TextWriter t) : base(t.FormatProvider)
			{
				this._out = t;
			}

			// Token: 0x17000952 RID: 2386
			// (get) Token: 0x06003743 RID: 14147 RVA: 0x000BAA84 File Offset: 0x000B9A84
			public override Encoding Encoding
			{
				get
				{
					return this._out.Encoding;
				}
			}

			// Token: 0x17000953 RID: 2387
			// (get) Token: 0x06003744 RID: 14148 RVA: 0x000BAA91 File Offset: 0x000B9A91
			public override IFormatProvider FormatProvider
			{
				get
				{
					return this._out.FormatProvider;
				}
			}

			// Token: 0x17000954 RID: 2388
			// (get) Token: 0x06003745 RID: 14149 RVA: 0x000BAA9E File Offset: 0x000B9A9E
			// (set) Token: 0x06003746 RID: 14150 RVA: 0x000BAAAB File Offset: 0x000B9AAB
			public override string NewLine
			{
				[MethodImpl(MethodImplOptions.Synchronized)]
				get
				{
					return this._out.NewLine;
				}
				[MethodImpl(MethodImplOptions.Synchronized)]
				set
				{
					this._out.NewLine = value;
				}
			}

			// Token: 0x06003747 RID: 14151 RVA: 0x000BAAB9 File Offset: 0x000B9AB9
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Close()
			{
				this._out.Close();
			}

			// Token: 0x06003748 RID: 14152 RVA: 0x000BAAC6 File Offset: 0x000B9AC6
			[MethodImpl(MethodImplOptions.Synchronized)]
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					((IDisposable)this._out).Dispose();
				}
			}

			// Token: 0x06003749 RID: 14153 RVA: 0x000BAAD6 File Offset: 0x000B9AD6
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Flush()
			{
				this._out.Flush();
			}

			// Token: 0x0600374A RID: 14154 RVA: 0x000BAAE3 File Offset: 0x000B9AE3
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char value)
			{
				this._out.Write(value);
			}

			// Token: 0x0600374B RID: 14155 RVA: 0x000BAAF1 File Offset: 0x000B9AF1
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char[] buffer)
			{
				this._out.Write(buffer);
			}

			// Token: 0x0600374C RID: 14156 RVA: 0x000BAAFF File Offset: 0x000B9AFF
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char[] buffer, int index, int count)
			{
				this._out.Write(buffer, index, count);
			}

			// Token: 0x0600374D RID: 14157 RVA: 0x000BAB0F File Offset: 0x000B9B0F
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(bool value)
			{
				this._out.Write(value);
			}

			// Token: 0x0600374E RID: 14158 RVA: 0x000BAB1D File Offset: 0x000B9B1D
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(int value)
			{
				this._out.Write(value);
			}

			// Token: 0x0600374F RID: 14159 RVA: 0x000BAB2B File Offset: 0x000B9B2B
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(uint value)
			{
				this._out.Write(value);
			}

			// Token: 0x06003750 RID: 14160 RVA: 0x000BAB39 File Offset: 0x000B9B39
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(long value)
			{
				this._out.Write(value);
			}

			// Token: 0x06003751 RID: 14161 RVA: 0x000BAB47 File Offset: 0x000B9B47
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(ulong value)
			{
				this._out.Write(value);
			}

			// Token: 0x06003752 RID: 14162 RVA: 0x000BAB55 File Offset: 0x000B9B55
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(float value)
			{
				this._out.Write(value);
			}

			// Token: 0x06003753 RID: 14163 RVA: 0x000BAB63 File Offset: 0x000B9B63
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(double value)
			{
				this._out.Write(value);
			}

			// Token: 0x06003754 RID: 14164 RVA: 0x000BAB71 File Offset: 0x000B9B71
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(decimal value)
			{
				this._out.Write(value);
			}

			// Token: 0x06003755 RID: 14165 RVA: 0x000BAB7F File Offset: 0x000B9B7F
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string value)
			{
				this._out.Write(value);
			}

			// Token: 0x06003756 RID: 14166 RVA: 0x000BAB8D File Offset: 0x000B9B8D
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(object value)
			{
				this._out.Write(value);
			}

			// Token: 0x06003757 RID: 14167 RVA: 0x000BAB9B File Offset: 0x000B9B9B
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0)
			{
				this._out.Write(format, arg0);
			}

			// Token: 0x06003758 RID: 14168 RVA: 0x000BABAA File Offset: 0x000B9BAA
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0, object arg1)
			{
				this._out.Write(format, arg0, arg1);
			}

			// Token: 0x06003759 RID: 14169 RVA: 0x000BABBA File Offset: 0x000B9BBA
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0, object arg1, object arg2)
			{
				this._out.Write(format, arg0, arg1, arg2);
			}

			// Token: 0x0600375A RID: 14170 RVA: 0x000BABCC File Offset: 0x000B9BCC
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object[] arg)
			{
				this._out.Write(format, arg);
			}

			// Token: 0x0600375B RID: 14171 RVA: 0x000BABDB File Offset: 0x000B9BDB
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine()
			{
				this._out.WriteLine();
			}

			// Token: 0x0600375C RID: 14172 RVA: 0x000BABE8 File Offset: 0x000B9BE8
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x0600375D RID: 14173 RVA: 0x000BABF6 File Offset: 0x000B9BF6
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(decimal value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x0600375E RID: 14174 RVA: 0x000BAC04 File Offset: 0x000B9C04
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char[] buffer)
			{
				this._out.WriteLine(buffer);
			}

			// Token: 0x0600375F RID: 14175 RVA: 0x000BAC12 File Offset: 0x000B9C12
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char[] buffer, int index, int count)
			{
				this._out.WriteLine(buffer, index, count);
			}

			// Token: 0x06003760 RID: 14176 RVA: 0x000BAC22 File Offset: 0x000B9C22
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(bool value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003761 RID: 14177 RVA: 0x000BAC30 File Offset: 0x000B9C30
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(int value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003762 RID: 14178 RVA: 0x000BAC3E File Offset: 0x000B9C3E
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(uint value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003763 RID: 14179 RVA: 0x000BAC4C File Offset: 0x000B9C4C
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(long value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003764 RID: 14180 RVA: 0x000BAC5A File Offset: 0x000B9C5A
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(ulong value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003765 RID: 14181 RVA: 0x000BAC68 File Offset: 0x000B9C68
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(float value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003766 RID: 14182 RVA: 0x000BAC76 File Offset: 0x000B9C76
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(double value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003767 RID: 14183 RVA: 0x000BAC84 File Offset: 0x000B9C84
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003768 RID: 14184 RVA: 0x000BAC92 File Offset: 0x000B9C92
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(object value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003769 RID: 14185 RVA: 0x000BACA0 File Offset: 0x000B9CA0
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0)
			{
				this._out.WriteLine(format, arg0);
			}

			// Token: 0x0600376A RID: 14186 RVA: 0x000BACAF File Offset: 0x000B9CAF
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0, object arg1)
			{
				this._out.WriteLine(format, arg0, arg1);
			}

			// Token: 0x0600376B RID: 14187 RVA: 0x000BACBF File Offset: 0x000B9CBF
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0, object arg1, object arg2)
			{
				this._out.WriteLine(format, arg0, arg1, arg2);
			}

			// Token: 0x0600376C RID: 14188 RVA: 0x000BACD1 File Offset: 0x000B9CD1
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object[] arg)
			{
				this._out.WriteLine(format, arg);
			}

			// Token: 0x04001CC6 RID: 7366
			private TextWriter _out;
		}
	}
}
