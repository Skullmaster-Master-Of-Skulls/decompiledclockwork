using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;

// Token: 0x02000007 RID: 7
public class SupportClass
{
	// Token: 0x0600003A RID: 58 RVA: 0x000036F0 File Offset: 0x000026F0
	[CLSCompliant(false)]
	public static sbyte[] ToSByteArray(byte[] byteArray)
	{
		sbyte[] array = new sbyte[byteArray.Length];
		for (int i = 0; i < byteArray.Length; i++)
		{
			array[i] = (sbyte)byteArray[i];
		}
		return array;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00003720 File Offset: 0x00002720
	[CLSCompliant(false)]
	public static byte[] ToByteArray(sbyte[] sbyteArray)
	{
		byte[] array = new byte[sbyteArray.Length];
		for (int i = 0; i < sbyteArray.Length; i++)
		{
			array[i] = (byte)sbyteArray[i];
		}
		return array;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00003750 File Offset: 0x00002750
	public static byte[] ToByteArray(string sourceString)
	{
		byte[] array = new byte[sourceString.Length];
		for (int i = 0; i < sourceString.Length; i++)
		{
			array[i] = (byte)sourceString[i];
		}
		return array;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x0000378C File Offset: 0x0000278C
	public static byte[] ToByteArray(object[] tempObjectArray)
	{
		byte[] array = new byte[tempObjectArray.Length];
		for (int i = 0; i < tempObjectArray.Length; i++)
		{
			array[i] = (byte)tempObjectArray[i];
		}
		return array;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x000037C4 File Offset: 0x000027C4
	[CLSCompliant(false)]
	public static int ReadInput(Stream sourceStream, ref sbyte[] target, int start, int count)
	{
		int result;
		if (target.Length == 0)
		{
			result = 0;
		}
		else
		{
			byte[] array = new byte[target.Length];
			int num = 0;
			int num2 = start;
			int num3;
			for (int i = count; i > 0; i -= num3)
			{
				num3 = sourceStream.Read(array, num2, i);
				if (num3 == 0)
				{
					break;
				}
				num += num3;
				num2 += num3;
			}
			if (num == 0)
			{
				result = -1;
			}
			else
			{
				for (int j = start; j < start + num; j++)
				{
					target[j] = (sbyte)array[j];
				}
				result = num;
			}
		}
		return result;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00003840 File Offset: 0x00002840
	[CLSCompliant(false)]
	public static int ReadInput(TextReader sourceTextReader, ref sbyte[] target, int start, int count)
	{
		int result;
		if (target.Length == 0)
		{
			result = 0;
		}
		else
		{
			char[] array = new char[target.Length];
			int num = sourceTextReader.Read(array, start, count);
			if (num == 0)
			{
				result = -1;
			}
			else
			{
				for (int i = start; i < start + num; i++)
				{
					target[i] = (sbyte)array[i];
				}
				result = num;
			}
		}
		return result;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00003890 File Offset: 0x00002890
	public static long Identity(long literal)
	{
		return literal;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x000038A4 File Offset: 0x000028A4
	[CLSCompliant(false)]
	public static ulong Identity(ulong literal)
	{
		return literal;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x000038B8 File Offset: 0x000028B8
	public static float Identity(float literal)
	{
		return literal;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x000038CC File Offset: 0x000028CC
	public static double Identity(double literal)
	{
		return literal;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x000038E0 File Offset: 0x000028E0
	public static string FormatDateTime(DateTimeFormatInfo format, DateTime date)
	{
		string timeFormatPattern = SupportClass.DateTimeFormatManager.manager.GetTimeFormatPattern(format);
		string dateFormatPattern = SupportClass.DateTimeFormatManager.manager.GetDateFormatPattern(format);
		return date.ToString(dateFormatPattern + " " + timeFormatPattern, format);
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00003920 File Offset: 0x00002920
	public static object PutElement(IDictionary collection, object key, object newValue)
	{
		object result = collection[key];
		collection[key] = newValue;
		return result;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00003944 File Offset: 0x00002944
	public static bool VectorRemoveElement(ArrayList arrayList, object element)
	{
		bool result = arrayList.Contains(element);
		arrayList.Remove(element);
		return result;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00003968 File Offset: 0x00002968
	public static object HashtableRemove(Hashtable hashtable, object key)
	{
		object result = hashtable[key];
		hashtable.Remove(key);
		return result;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x0000398C File Offset: 0x0000298C
	public static void SetSize(ArrayList arrayList, int newSize)
	{
		if (newSize < 0)
		{
			throw new ArgumentException();
		}
		if (newSize < arrayList.Count)
		{
			arrayList.RemoveRange(newSize, arrayList.Count - newSize);
		}
		else
		{
			while (newSize > arrayList.Count)
			{
				arrayList.Add(null);
			}
		}
	}

	// Token: 0x06000049 RID: 73 RVA: 0x000039D0 File Offset: 0x000029D0
	public static object StackPush(Stack stack, object element)
	{
		stack.Push(element);
		return element;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x000039EC File Offset: 0x000029EC
	public static void GetCharsFromString(string sourceString, int sourceStart, int sourceEnd, ref char[] destinationArray, int destinationStart)
	{
		int i = sourceStart;
		int num = destinationStart;
		while (i < sourceEnd)
		{
			destinationArray[num] = sourceString[i];
			i++;
			num++;
		}
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00003A18 File Offset: 0x00002A18
	public static FileStream GetFileStream(string FileName, bool Append)
	{
		FileStream result;
		if (Append)
		{
			result = new FileStream(FileName, FileMode.Append);
		}
		else
		{
			result = new FileStream(FileName, FileMode.Create);
		}
		return result;
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00003A40 File Offset: 0x00002A40
	[CLSCompliant(false)]
	public static char[] ToCharArray(sbyte[] sByteArray)
	{
		char[] array = new char[sByteArray.Length];
		sByteArray.CopyTo(array, 0);
		return array;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00003A64 File Offset: 0x00002A64
	public static char[] ToCharArray(byte[] byteArray)
	{
		char[] array = new char[byteArray.Length];
		byteArray.CopyTo(array, 0);
		return array;
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00003A88 File Offset: 0x00002A88
	public static object CreateNewInstance(Type classType)
	{
		object result = null;
		Type[] array = new Type[0];
		Type[] types = array;
		ConstructorInfo[] constructors = classType.GetConstructors();
		if (constructors.Length == 0)
		{
			throw new UnauthorizedAccessException();
		}
		for (int i = 0; i < constructors.Length; i++)
		{
			ParameterInfo[] parameters = constructors[i].GetParameters();
			if (parameters.Length == 0)
			{
				ConstructorInfo constructor = classType.GetConstructor(types);
				object[] parameters2 = new object[0];
				result = constructor.Invoke(parameters2);
				break;
			}
			if (i == constructors.Length - 1)
			{
				throw new MethodAccessException();
			}
		}
		return result;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00003B04 File Offset: 0x00002B04
	public static void WriteStackTrace(Exception throwable, TextWriter stream)
	{
		stream.Write(throwable.StackTrace);
		stream.Flush();
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00003B24 File Offset: 0x00002B24
	public static bool EqualsSupport(ICollection source, ICollection target)
	{
		IEnumerator enumerator = SupportClass.ReverseStack(source);
		IEnumerator enumerator2 = SupportClass.ReverseStack(target);
		bool result;
		if (source.Count != target.Count)
		{
			result = false;
		}
		else
		{
			while (enumerator.MoveNext() && enumerator2.MoveNext())
			{
				if (!enumerator.Current.Equals(enumerator2.Current))
				{
					return false;
				}
			}
			result = true;
		}
		return result;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00003B80 File Offset: 0x00002B80
	public static bool EqualsSupport(ICollection source, object target)
	{
		return target.GetType() == typeof(ICollection) && SupportClass.EqualsSupport(source, (ICollection)target);
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00003BB4 File Offset: 0x00002BB4
	public static bool EqualsSupport(IDictionaryEnumerator source, object target)
	{
		return target.GetType() == typeof(IDictionaryEnumerator) && SupportClass.EqualsSupport(source, (IDictionaryEnumerator)target);
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00003BE8 File Offset: 0x00002BE8
	public static bool EqualsSupport(IDictionaryEnumerator source, IDictionaryEnumerator target)
	{
		while (source.MoveNext() && target.MoveNext())
		{
			if (source.Key.Equals(target.Key) && source.Value.Equals(target.Value))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00003C38 File Offset: 0x00002C38
	public static IEnumerator ReverseStack(ICollection collection)
	{
		IEnumerator enumerator;
		if (collection.GetType() == typeof(Stack))
		{
			ArrayList arrayList = new ArrayList(collection);
			arrayList.Reverse();
			enumerator = arrayList.GetEnumerator();
		}
		else
		{
			enumerator = collection.GetEnumerator();
		}
		return enumerator;
	}

	// Token: 0x02000008 RID: 8
	public class Tokenizer
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00003C8C File Offset: 0x00002C8C
		public Tokenizer(string source)
		{
			this.elements = new ArrayList();
			this.elements.AddRange(source.Split(this.delimiters.ToCharArray()));
			this.RemoveEmptyStrings();
			this.source = source;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003CE8 File Offset: 0x00002CE8
		public Tokenizer(string source, string delimiters)
		{
			this.elements = new ArrayList();
			this.delimiters = delimiters;
			this.elements.AddRange(source.Split(this.delimiters.ToCharArray()));
			this.RemoveEmptyStrings();
			this.source = source;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003D48 File Offset: 0x00002D48
		public Tokenizer(string source, string delimiters, bool retDel)
		{
			this.elements = new ArrayList();
			this.delimiters = delimiters;
			this.source = source;
			this.returnDelims = retDel;
			if (this.returnDelims)
			{
				this.Tokenize();
			}
			else
			{
				this.elements.AddRange(source.Split(this.delimiters.ToCharArray()));
			}
			this.RemoveEmptyStrings();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003DC0 File Offset: 0x00002DC0
		private void Tokenize()
		{
			string text = this.source;
			if (text.IndexOfAny(this.delimiters.ToCharArray()) < 0 && text.Length > 0)
			{
				this.elements.Add(text);
			}
			else if (text.IndexOfAny(this.delimiters.ToCharArray()) < 0 && text.Length <= 0)
			{
				return;
			}
			while (text.IndexOfAny(this.delimiters.ToCharArray()) >= 0)
			{
				if (text.IndexOfAny(this.delimiters.ToCharArray()) == 0)
				{
					if (text.Length > 1)
					{
						this.elements.Add(text.Substring(0, 1));
						text = text.Substring(1);
					}
					else
					{
						text = "";
					}
				}
				else
				{
					string text2 = text.Substring(0, text.IndexOfAny(this.delimiters.ToCharArray()));
					this.elements.Add(text2);
					this.elements.Add(text.Substring(text2.Length, 1));
					if (text.Length > text2.Length + 1)
					{
						text = text.Substring(text2.Length + 1);
					}
					else
					{
						text = "";
					}
				}
			}
			if (text.Length > 0)
			{
				this.elements.Add(text);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003F04 File Offset: 0x00002F04
		public int Count
		{
			get
			{
				return this.elements.Count;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003F20 File Offset: 0x00002F20
		public bool HasMoreTokens()
		{
			return this.elements.Count > 0;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003F40 File Offset: 0x00002F40
		public string NextToken()
		{
			if (this.source == "")
			{
				throw new Exception();
			}
			string result;
			if (this.returnDelims)
			{
				this.RemoveEmptyStrings();
				string text = (string)this.elements[0];
				this.elements.RemoveAt(0);
				result = text;
			}
			else
			{
				this.elements = new ArrayList();
				this.elements.AddRange(this.source.Split(this.delimiters.ToCharArray()));
				this.RemoveEmptyStrings();
				string text = (string)this.elements[0];
				this.elements.RemoveAt(0);
				this.source = this.source.Remove(this.source.IndexOf(text), text.Length);
				this.source = this.source.TrimStart(this.delimiters.ToCharArray());
				result = text;
			}
			return result;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000402C File Offset: 0x0000302C
		public string NextToken(string delimiters)
		{
			this.delimiters = delimiters;
			return this.NextToken();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000404C File Offset: 0x0000304C
		private void RemoveEmptyStrings()
		{
			for (int i = 0; i < this.elements.Count; i++)
			{
				if ((string)this.elements[i] == "")
				{
					this.elements.RemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x04000037 RID: 55
		private ArrayList elements;

		// Token: 0x04000038 RID: 56
		private string source;

		// Token: 0x04000039 RID: 57
		private string delimiters = " \t\n\r";

		// Token: 0x0400003A RID: 58
		private bool returnDelims = false;
	}

	// Token: 0x02000009 RID: 9
	public class DateTimeFormatManager
	{
		// Token: 0x0400003B RID: 59
		public static SupportClass.DateTimeFormatManager.DateTimeFormatHashTable manager = new SupportClass.DateTimeFormatManager.DateTimeFormatHashTable();

		// Token: 0x0200000A RID: 10
		public class DateTimeFormatHashTable : Hashtable
		{
			// Token: 0x06000061 RID: 97 RVA: 0x000040C8 File Offset: 0x000030C8
			public void SetDateFormatPattern(DateTimeFormatInfo format, string newPattern)
			{
				if (this[format] != null)
				{
					((SupportClass.DateTimeFormatManager.DateTimeFormatHashTable.DateTimeFormatProperties)this[format]).DateFormatPattern = newPattern;
				}
				else
				{
					this.Add(format, new SupportClass.DateTimeFormatManager.DateTimeFormatHashTable.DateTimeFormatProperties
					{
						DateFormatPattern = newPattern
					});
				}
			}

			// Token: 0x06000062 RID: 98 RVA: 0x00004108 File Offset: 0x00003108
			public string GetDateFormatPattern(DateTimeFormatInfo format)
			{
				string result;
				if (this[format] == null)
				{
					result = "d-MMM-yy";
				}
				else
				{
					result = ((SupportClass.DateTimeFormatManager.DateTimeFormatHashTable.DateTimeFormatProperties)this[format]).DateFormatPattern;
				}
				return result;
			}

			// Token: 0x06000063 RID: 99 RVA: 0x0000413C File Offset: 0x0000313C
			public void SetTimeFormatPattern(DateTimeFormatInfo format, string newPattern)
			{
				if (this[format] != null)
				{
					((SupportClass.DateTimeFormatManager.DateTimeFormatHashTable.DateTimeFormatProperties)this[format]).TimeFormatPattern = newPattern;
				}
				else
				{
					this.Add(format, new SupportClass.DateTimeFormatManager.DateTimeFormatHashTable.DateTimeFormatProperties
					{
						TimeFormatPattern = newPattern
					});
				}
			}

			// Token: 0x06000064 RID: 100 RVA: 0x0000417C File Offset: 0x0000317C
			public string GetTimeFormatPattern(DateTimeFormatInfo format)
			{
				string result;
				if (this[format] == null)
				{
					result = "h:mm:ss tt";
				}
				else
				{
					result = ((SupportClass.DateTimeFormatManager.DateTimeFormatHashTable.DateTimeFormatProperties)this[format]).TimeFormatPattern;
				}
				return result;
			}

			// Token: 0x0200000B RID: 11
			private class DateTimeFormatProperties
			{
				// Token: 0x0400003C RID: 60
				public string DateFormatPattern = "d-MMM-yy";

				// Token: 0x0400003D RID: 61
				public string TimeFormatPattern = "h:mm:ss tt";
			}
		}
	}

	// Token: 0x0200000C RID: 12
	public class ArrayListSupport
	{
		// Token: 0x06000067 RID: 103 RVA: 0x000041F0 File Offset: 0x000031F0
		public static object[] ToArray(ArrayList collection, object[] objects)
		{
			int num = 0;
			foreach (object obj in collection)
			{
				objects[num++] = obj;
			}
			return objects;
		}
	}

	// Token: 0x0200000D RID: 13
	public class ThreadClass : IThreadRunnable
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00004238 File Offset: 0x00003238
		public ThreadClass()
		{
			this.threadField = new Thread(new ThreadStart(this.Run));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004264 File Offset: 0x00003264
		public ThreadClass(string Name)
		{
			this.threadField = new Thread(new ThreadStart(this.Run));
			this.Name = Name;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004298 File Offset: 0x00003298
		public ThreadClass(ThreadStart Start)
		{
			this.threadField = new Thread(Start);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000042B8 File Offset: 0x000032B8
		public ThreadClass(ThreadStart Start, string Name)
		{
			this.threadField = new Thread(Start);
			this.Name = Name;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000042E0 File Offset: 0x000032E0
		public virtual void Run()
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000042F0 File Offset: 0x000032F0
		public virtual void Start()
		{
			this.threadField.Start();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004308 File Offset: 0x00003308
		public virtual void Interrupt()
		{
			this.threadField.Interrupt();
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00004320 File Offset: 0x00003320
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00004338 File Offset: 0x00003338
		public Thread Instance
		{
			get
			{
				return this.threadField;
			}
			set
			{
				this.threadField = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000434C File Offset: 0x0000334C
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00004368 File Offset: 0x00003368
		public string Name
		{
			get
			{
				return this.threadField.Name;
			}
			set
			{
				if (this.threadField.Name == null)
				{
					this.threadField.Name = value;
				}
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00004390 File Offset: 0x00003390
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000043AC File Offset: 0x000033AC
		public ThreadPriority Priority
		{
			get
			{
				return this.threadField.Priority;
			}
			set
			{
				this.threadField.Priority = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000043C8 File Offset: 0x000033C8
		public bool IsAlive
		{
			get
			{
				return this.threadField.IsAlive;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000043E4 File Offset: 0x000033E4
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00004400 File Offset: 0x00003400
		public bool IsBackground
		{
			get
			{
				return this.threadField.IsBackground;
			}
			set
			{
				this.threadField.IsBackground = value;
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000441C File Offset: 0x0000341C
		public void Join()
		{
			this.threadField.Join();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004434 File Offset: 0x00003434
		public void Join(long MiliSeconds)
		{
			lock (this)
			{
				this.threadField.Join(new TimeSpan(MiliSeconds * 10000L));
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004488 File Offset: 0x00003488
		public void Join(long MiliSeconds, int NanoSeconds)
		{
			lock (this)
			{
				this.threadField.Join(new TimeSpan(MiliSeconds * 10000L + (long)(NanoSeconds * 100)));
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000044E4 File Offset: 0x000034E4
		public void Resume()
		{
			this.threadField.Resume();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000044FC File Offset: 0x000034FC
		public void Abort()
		{
			this.threadField.Abort();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004514 File Offset: 0x00003514
		public void Abort(object stateInfo)
		{
			lock (this)
			{
				this.threadField.Abort(stateInfo);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000455C File Offset: 0x0000355C
		public void Suspend()
		{
			this.threadField.Suspend();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004574 File Offset: 0x00003574
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Thread[",
				this.Name,
				",",
				this.Priority.ToString(),
				",]"
			});
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000045C8 File Offset: 0x000035C8
		public static SupportClass.ThreadClass Current()
		{
			return new SupportClass.ThreadClass
			{
				Instance = Thread.CurrentThread
			};
		}

		// Token: 0x0400003E RID: 62
		private Thread threadField;
	}

	// Token: 0x0200000E RID: 14
	public class CollectionSupport : CollectionBase
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00004600 File Offset: 0x00003600
		public virtual bool Add(object element)
		{
			return base.List.Add(element) != -1;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004624 File Offset: 0x00003624
		public virtual bool AddAll(ICollection collection)
		{
			bool result = false;
			if (collection != null)
			{
				IEnumerator enumerator = new ArrayList(collection).GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current != null)
					{
						result = this.Add(enumerator.Current);
					}
				}
			}
			return result;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004668 File Offset: 0x00003668
		public virtual bool AddAll(SupportClass.CollectionSupport collection)
		{
			return this.AddAll(collection);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004680 File Offset: 0x00003680
		public virtual bool Contains(object element)
		{
			return base.List.Contains(element);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000046A0 File Offset: 0x000036A0
		public virtual bool ContainsAll(ICollection collection)
		{
			bool result = false;
			IEnumerator enumerator = new ArrayList(collection).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!(result = this.Contains(enumerator.Current)))
				{
					break;
				}
			}
			return result;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000046DC File Offset: 0x000036DC
		public virtual bool ContainsAll(SupportClass.CollectionSupport collection)
		{
			return this.ContainsAll(collection);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000046F4 File Offset: 0x000036F4
		public virtual bool IsEmpty()
		{
			return base.Count == 0;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004710 File Offset: 0x00003710
		public virtual bool Remove(object element)
		{
			bool result = false;
			if (this.Contains(element))
			{
				base.List.Remove(element);
				result = true;
			}
			return result;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000473C File Offset: 0x0000373C
		public virtual bool RemoveAll(ICollection collection)
		{
			bool result = false;
			IEnumerator enumerator = new ArrayList(collection).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (this.Contains(enumerator.Current))
				{
					result = this.Remove(enumerator.Current);
				}
			}
			return result;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004784 File Offset: 0x00003784
		public virtual bool RemoveAll(SupportClass.CollectionSupport collection)
		{
			return this.RemoveAll(collection);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000479C File Offset: 0x0000379C
		public virtual bool RetainAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = base.GetEnumerator();
			SupportClass.CollectionSupport collectionSupport = new SupportClass.CollectionSupport();
			collectionSupport.AddAll(collection);
			while (enumerator.MoveNext())
			{
				if (!collectionSupport.Contains(enumerator.Current))
				{
					flag = this.Remove(enumerator.Current);
					if (flag)
					{
						enumerator = base.GetEnumerator();
					}
				}
			}
			return flag;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000047F4 File Offset: 0x000037F4
		public virtual bool RetainAll(SupportClass.CollectionSupport collection)
		{
			return this.RetainAll(collection);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000480C File Offset: 0x0000380C
		public virtual object[] ToArray()
		{
			int num = 0;
			object[] array = new object[base.Count];
			foreach (object obj in this)
			{
				array[num++] = obj;
			}
			return array;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000484C File Offset: 0x0000384C
		public virtual object[] ToArray(object[] objects)
		{
			int num = 0;
			foreach (object obj in this)
			{
				objects[num++] = obj;
			}
			return objects;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004880 File Offset: 0x00003880
		public static SupportClass.CollectionSupport ToCollectionSupport(object[] array)
		{
			SupportClass.CollectionSupport collectionSupport = new SupportClass.CollectionSupport();
			collectionSupport.AddAll(array);
			return collectionSupport;
		}
	}

	// Token: 0x0200000F RID: 15
	public class ListCollectionSupport : ArrayList
	{
		// Token: 0x06000092 RID: 146 RVA: 0x000048A0 File Offset: 0x000038A0
		public ListCollectionSupport()
		{
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000048B4 File Offset: 0x000038B4
		public ListCollectionSupport(ICollection collection) : base(collection)
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000048C8 File Offset: 0x000038C8
		public ListCollectionSupport(int capacity) : base(capacity)
		{
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000048DC File Offset: 0x000038DC
		public new virtual bool Add(object valueToInsert)
		{
			base.Insert(this.Count, valueToInsert);
			return true;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000048FC File Offset: 0x000038FC
		public virtual bool AddAll(int index, IList list)
		{
			bool result = false;
			if (list != null)
			{
				IEnumerator enumerator = new ArrayList(list).GetEnumerator();
				int num = index;
				while (enumerator.MoveNext())
				{
					object value = enumerator.Current;
					base.Insert(num++, value);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004940 File Offset: 0x00003940
		public virtual bool AddAll(IList collection)
		{
			return this.AddAll(this.Count, collection);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004960 File Offset: 0x00003960
		public virtual bool AddAll(SupportClass.CollectionSupport collection)
		{
			return this.AddAll(this.Count, collection);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004980 File Offset: 0x00003980
		public virtual bool AddAll(int index, SupportClass.CollectionSupport collection)
		{
			return this.AddAll(index, collection);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000499C File Offset: 0x0000399C
		public virtual object ListCollectionClone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000049B4 File Offset: 0x000039B4
		public virtual IEnumerator ListIterator()
		{
			return base.GetEnumerator();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000049CC File Offset: 0x000039CC
		public virtual bool RemoveAll(ICollection collection)
		{
			bool result = false;
			IEnumerator enumerator = new ArrayList(collection).GetEnumerator();
			while (enumerator.MoveNext())
			{
				result = true;
				if (base.Contains(enumerator.Current))
				{
					base.Remove(enumerator.Current);
				}
			}
			return result;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004A14 File Offset: 0x00003A14
		public virtual bool RemoveAll(SupportClass.CollectionSupport collection)
		{
			return this.RemoveAll(collection);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004A2C File Offset: 0x00003A2C
		public virtual object RemoveElement(int index)
		{
			object result = this[index];
			this.RemoveAt(index);
			return result;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004A50 File Offset: 0x00003A50
		public virtual bool RemoveElement(object element)
		{
			bool result = false;
			if (this.Contains(element))
			{
				base.Remove(element);
				result = true;
			}
			return result;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004A78 File Offset: 0x00003A78
		public virtual object RemoveFirst()
		{
			object result = this[0];
			this.RemoveAt(0);
			return result;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004A9C File Offset: 0x00003A9C
		public virtual object RemoveLast()
		{
			object result = this[this.Count - 1];
			base.RemoveAt(this.Count - 1);
			return result;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004ACC File Offset: 0x00003ACC
		public virtual bool RetainAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = this.GetEnumerator();
			SupportClass.ListCollectionSupport listCollectionSupport = new SupportClass.ListCollectionSupport(collection);
			while (enumerator.MoveNext())
			{
				if (!listCollectionSupport.Contains(enumerator.Current))
				{
					flag = this.RemoveElement(enumerator.Current);
					if (flag)
					{
						enumerator = this.GetEnumerator();
					}
				}
			}
			return flag;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004B20 File Offset: 0x00003B20
		public virtual bool RetainAll(SupportClass.CollectionSupport collection)
		{
			return this.RetainAll(collection);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004B38 File Offset: 0x00003B38
		public virtual bool ContainsAll(ICollection collection)
		{
			bool result = false;
			IEnumerator enumerator = new ArrayList(collection).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!(result = this.Contains(enumerator.Current)))
				{
					break;
				}
			}
			return result;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004B74 File Offset: 0x00003B74
		public virtual bool ContainsAll(SupportClass.CollectionSupport collection)
		{
			return this.ContainsAll(collection);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004B8C File Offset: 0x00003B8C
		public virtual SupportClass.ListCollectionSupport SubList(int startIndex, int endIndex)
		{
			IEnumerator enumerator = this.GetEnumerator();
			SupportClass.ListCollectionSupport listCollectionSupport = new SupportClass.ListCollectionSupport();
			for (int i = startIndex; i < endIndex; i++)
			{
				listCollectionSupport.Add(this[i]);
			}
			return listCollectionSupport;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004BC8 File Offset: 0x00003BC8
		public virtual object[] ToArray(object[] objects)
		{
			if (objects.Length < this.Count)
			{
				objects = new object[this.Count];
			}
			int num = 0;
			foreach (object obj in this)
			{
				objects[num++] = obj;
			}
			return objects;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004C14 File Offset: 0x00003C14
		public virtual IEnumerator ListIterator(int index)
		{
			if (index < 0 || index > this.Count)
			{
				throw new IndexOutOfRangeException();
			}
			IEnumerator enumerator = this.GetEnumerator();
			if (index > 0)
			{
				int num = 0;
				while (enumerator.MoveNext() && num < index - 1)
				{
					num++;
				}
			}
			return enumerator;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004C5C File Offset: 0x00003C5C
		public virtual object GetLast()
		{
			if (this.Count == 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this[this.Count - 1];
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004C8C File Offset: 0x00003C8C
		public virtual bool IsEmpty()
		{
			return this.Count == 0;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004CA8 File Offset: 0x00003CA8
		public virtual object Set(int index, object element)
		{
			object result = this[index];
			this[index] = element;
			return result;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004CCC File Offset: 0x00003CCC
		public virtual object Get(int index)
		{
			return this[index];
		}
	}

	// Token: 0x02000010 RID: 16
	public class ArraysSupport
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00004CE4 File Offset: 0x00003CE4
		public static bool IsArrayEqual(Array array1, Array array2)
		{
			bool result;
			if (array1.Length != array2.Length)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < array1.Length; i++)
				{
					if (!array1.GetValue(i).Equals(array2.GetValue(i)))
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004D34 File Offset: 0x00003D34
		public static void FillArray(Array array, int fromindex, int toindex, object val)
		{
			object value = val;
			Type elementType = array.GetType().GetElementType();
			if (elementType != val.GetType())
			{
				value = Convert.ChangeType(val, elementType);
			}
			if (array.Length == 0)
			{
				throw new NullReferenceException();
			}
			if (fromindex > toindex)
			{
				throw new ArgumentException();
			}
			if (fromindex < 0 || array.Length < toindex)
			{
				throw new IndexOutOfRangeException();
			}
			int num;
			if (fromindex <= 0)
			{
				num = fromindex;
			}
			else
			{
				fromindex = (num = fromindex) - 1;
			}
			for (int i = num; i < toindex; i++)
			{
				array.SetValue(value, i);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004DAC File Offset: 0x00003DAC
		public static void FillArray(Array array, object val)
		{
			SupportClass.ArraysSupport.FillArray(array, 0, array.Length, val);
		}
	}

	// Token: 0x02000011 RID: 17
	public class SetSupport : ArrayList
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00004DDC File Offset: 0x00003DDC
		public SetSupport()
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004DF0 File Offset: 0x00003DF0
		public SetSupport(ICollection collection) : base(collection)
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004E04 File Offset: 0x00003E04
		public SetSupport(int capacity) : base(capacity)
		{
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004E18 File Offset: 0x00003E18
		public new virtual bool Add(object objectToAdd)
		{
			bool result;
			if (this.Contains(objectToAdd))
			{
				result = false;
			}
			else
			{
				base.Add(objectToAdd);
				result = true;
			}
			return result;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004E40 File Offset: 0x00003E40
		public virtual bool AddAll(ICollection collection)
		{
			bool result = false;
			if (collection != null)
			{
				IEnumerator enumerator = new ArrayList(collection).GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current != null)
					{
						result = this.Add(enumerator.Current);
					}
				}
			}
			return result;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004E84 File Offset: 0x00003E84
		public virtual bool AddAll(SupportClass.CollectionSupport collection)
		{
			return this.AddAll(collection);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004E9C File Offset: 0x00003E9C
		public virtual bool ContainsAll(ICollection collection)
		{
			bool result = false;
			IEnumerator enumerator = collection.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!(result = this.Contains(enumerator.Current)))
				{
					break;
				}
			}
			return result;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004ED4 File Offset: 0x00003ED4
		public virtual bool ContainsAll(SupportClass.CollectionSupport collection)
		{
			return this.ContainsAll(collection);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004EEC File Offset: 0x00003EEC
		public virtual bool IsEmpty()
		{
			return this.Count == 0;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004F08 File Offset: 0x00003F08
		public new virtual bool Remove(object elementToRemove)
		{
			bool result = false;
			if (this.Contains(elementToRemove))
			{
				result = true;
			}
			base.Remove(elementToRemove);
			return result;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004F30 File Offset: 0x00003F30
		public virtual bool RemoveAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = collection.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!flag && this.Contains(enumerator.Current))
				{
					flag = true;
				}
				this.Remove(enumerator.Current);
			}
			return flag;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004F78 File Offset: 0x00003F78
		public virtual bool RemoveAll(SupportClass.CollectionSupport collection)
		{
			return this.RemoveAll(collection);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004F90 File Offset: 0x00003F90
		public virtual bool RetainAll(ICollection collection)
		{
			bool result = false;
			IEnumerator enumerator = collection.GetEnumerator();
			SupportClass.SetSupport setSupport = (SupportClass.SetSupport)collection;
			while (enumerator.MoveNext())
			{
				if (!setSupport.Contains(enumerator.Current))
				{
					result = this.Remove(enumerator.Current);
					enumerator = this.GetEnumerator();
				}
			}
			return result;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004FE0 File Offset: 0x00003FE0
		public virtual bool RetainAll(SupportClass.CollectionSupport collection)
		{
			return this.RetainAll(collection);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004FF8 File Offset: 0x00003FF8
		public new virtual object[] ToArray()
		{
			int num = 0;
			object[] array = new object[this.Count];
			foreach (object obj in this)
			{
				array[num++] = obj;
			}
			return array;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005038 File Offset: 0x00004038
		public virtual object[] ToArray(object[] objects)
		{
			int num = 0;
			foreach (object obj in this)
			{
				objects[num++] = obj;
			}
			return objects;
		}
	}

	// Token: 0x02000012 RID: 18
	public class AbstractSetSupport : SupportClass.SetSupport
	{
	}

	// Token: 0x02000013 RID: 19
	public class MessageDigestSupport
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00005080 File Offset: 0x00004080
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00005098 File Offset: 0x00004098
		public HashAlgorithm Algorithm
		{
			get
			{
				return this.algorithm;
			}
			set
			{
				this.algorithm = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000050AC File Offset: 0x000040AC
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000050C4 File Offset: 0x000040C4
		public byte[] Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000050D8 File Offset: 0x000040D8
		public string AlgorithmName
		{
			get
			{
				return this.algorithmName;
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000050F0 File Offset: 0x000040F0
		public MessageDigestSupport(string algorithm)
		{
			if (algorithm.Equals("SHA-1"))
			{
				this.algorithmName = "SHA";
			}
			else
			{
				this.algorithmName = algorithm;
			}
			this.Algorithm = (HashAlgorithm)CryptoConfig.CreateFromName(this.algorithmName);
			this.position = 0;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005144 File Offset: 0x00004144
		[CLSCompliant(false)]
		public sbyte[] DigestData()
		{
			sbyte[] result = SupportClass.ToSByteArray(this.Algorithm.ComputeHash(this.data));
			this.Reset();
			return result;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005174 File Offset: 0x00004174
		[CLSCompliant(false)]
		public sbyte[] DigestData(byte[] newData)
		{
			this.Update(newData);
			return this.DigestData();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005194 File Offset: 0x00004194
		public void Update(byte[] newData)
		{
			if (this.position == 0)
			{
				this.Data = newData;
				this.position = this.Data.Length - 1;
			}
			else
			{
				byte[] array = this.Data;
				this.Data = new byte[newData.Length + this.position + 1];
				array.CopyTo(this.Data, 0);
				newData.CopyTo(this.Data, array.Length);
				this.position = this.Data.Length - 1;
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000520C File Offset: 0x0000420C
		public void Update(byte newData)
		{
			this.Update(new byte[]
			{
				newData
			});
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000522C File Offset: 0x0000422C
		public void Update(byte[] newData, int offset, int count)
		{
			byte[] array = new byte[count];
			Array.Copy(newData, offset, array, 0, count);
			this.Update(array);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005254 File Offset: 0x00004254
		public void Reset()
		{
			this.data = null;
			this.position = 0;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005270 File Offset: 0x00004270
		public override string ToString()
		{
			return this.Algorithm.ToString();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000528C File Offset: 0x0000428C
		public static SupportClass.MessageDigestSupport GetInstance(string algorithm)
		{
			return new SupportClass.MessageDigestSupport(algorithm);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000052A4 File Offset: 0x000042A4
		[CLSCompliant(false)]
		public static bool EquivalentDigest(sbyte[] firstDigest, sbyte[] secondDigest)
		{
			bool flag = false;
			if (firstDigest.Length == secondDigest.Length)
			{
				int num = 0;
				flag = true;
				while (flag && num < firstDigest.Length)
				{
					flag = (firstDigest[num] == secondDigest[num]);
					num++;
				}
			}
			return flag;
		}

		// Token: 0x0400003F RID: 63
		private HashAlgorithm algorithm;

		// Token: 0x04000040 RID: 64
		private byte[] data;

		// Token: 0x04000041 RID: 65
		private int position;

		// Token: 0x04000042 RID: 66
		private string algorithmName;
	}

	// Token: 0x02000014 RID: 20
	public class SecureRandomSupport
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x000052DC File Offset: 0x000042DC
		public SecureRandomSupport()
		{
			this.generator = new RNGCryptoServiceProvider();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000052FC File Offset: 0x000042FC
		public SecureRandomSupport(byte[] seed)
		{
			this.generator = new RNGCryptoServiceProvider(seed);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000531C File Offset: 0x0000431C
		[CLSCompliant(false)]
		public sbyte[] NextBytes(byte[] randomnumbersarray)
		{
			this.generator.GetBytes(randomnumbersarray);
			return SupportClass.ToSByteArray(randomnumbersarray);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005340 File Offset: 0x00004340
		public static byte[] GetSeed(int numberOfBytes)
		{
			RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
			byte[] array = new byte[numberOfBytes];
			rngcryptoServiceProvider.GetBytes(array);
			return array;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005368 File Offset: 0x00004368
		public void SetSeed(byte[] newSeed)
		{
			this.generator = new RNGCryptoServiceProvider(newSeed);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005384 File Offset: 0x00004384
		public void SetSeed(long newSeed)
		{
			byte[] array = new byte[8];
			for (int i = 7; i > 0; i--)
			{
				array[i] = (byte)(newSeed - (newSeed >> 8 << 8));
				newSeed >>= 8;
			}
			this.SetSeed(array);
		}

		// Token: 0x04000043 RID: 67
		private RNGCryptoServiceProvider generator;
	}

	// Token: 0x02000015 RID: 21
	public interface SingleThreadModel
	{
	}
}
