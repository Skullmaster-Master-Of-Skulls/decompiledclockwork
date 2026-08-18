using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Security.Permissions;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000692 RID: 1682
	internal abstract class RegexCompiler
	{
		// Token: 0x06003E44 RID: 15940 RVA: 0x00100D90 File Offset: 0x000FEF90
		static RegexCompiler()
		{
			new ReflectionPermission(PermissionState.Unrestricted).Assert();
			try
			{
				RegexCompiler._textbegF = RegexCompiler.RegexRunnerField("runtextbeg");
				RegexCompiler._textendF = RegexCompiler.RegexRunnerField("runtextend");
				RegexCompiler._textstartF = RegexCompiler.RegexRunnerField("runtextstart");
				RegexCompiler._textposF = RegexCompiler.RegexRunnerField("runtextpos");
				RegexCompiler._textF = RegexCompiler.RegexRunnerField("runtext");
				RegexCompiler._trackposF = RegexCompiler.RegexRunnerField("runtrackpos");
				RegexCompiler._trackF = RegexCompiler.RegexRunnerField("runtrack");
				RegexCompiler._stackposF = RegexCompiler.RegexRunnerField("runstackpos");
				RegexCompiler._stackF = RegexCompiler.RegexRunnerField("runstack");
				RegexCompiler._trackcountF = RegexCompiler.RegexRunnerField("runtrackcount");
				RegexCompiler._ensurestorageM = RegexCompiler.RegexRunnerMethod("EnsureStorage");
				RegexCompiler._captureM = RegexCompiler.RegexRunnerMethod("Capture");
				RegexCompiler._transferM = RegexCompiler.RegexRunnerMethod("TransferCapture");
				RegexCompiler._uncaptureM = RegexCompiler.RegexRunnerMethod("Uncapture");
				RegexCompiler._ismatchedM = RegexCompiler.RegexRunnerMethod("IsMatched");
				RegexCompiler._matchlengthM = RegexCompiler.RegexRunnerMethod("MatchLength");
				RegexCompiler._matchindexM = RegexCompiler.RegexRunnerMethod("MatchIndex");
				RegexCompiler._isboundaryM = RegexCompiler.RegexRunnerMethod("IsBoundary");
				RegexCompiler._charInSetM = RegexCompiler.RegexRunnerMethod("CharInClass");
				RegexCompiler._isECMABoundaryM = RegexCompiler.RegexRunnerMethod("IsECMABoundary");
				RegexCompiler._crawlposM = RegexCompiler.RegexRunnerMethod("Crawlpos");
				RegexCompiler._checkTimeoutM = RegexCompiler.RegexRunnerMethod("CheckTimeout");
				RegexCompiler._chartolowerM = typeof(char).GetMethod("ToLower", new Type[]
				{
					typeof(char),
					typeof(CultureInfo)
				});
				RegexCompiler._getcharM = typeof(string).GetMethod("get_Chars", new Type[]
				{
					typeof(int)
				});
				RegexCompiler._getCurrentCulture = typeof(CultureInfo).GetMethod("get_CurrentCulture");
				RegexCompiler._getInvariantCulture = typeof(CultureInfo).GetMethod("get_InvariantCulture");
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x00100FB8 File Offset: 0x000FF1B8
		private static FieldInfo RegexRunnerField(string fieldname)
		{
			return typeof(RegexRunner).GetField(fieldname, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x00100FCC File Offset: 0x000FF1CC
		private static MethodInfo RegexRunnerMethod(string methname)
		{
			return typeof(RegexRunner).GetMethod(methname, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x00100FE0 File Offset: 0x000FF1E0
		internal static RegexRunnerFactory Compile(RegexCode code, RegexOptions options)
		{
			RegexLWCGCompiler regexLWCGCompiler = new RegexLWCGCompiler();
			new ReflectionPermission(PermissionState.Unrestricted).Assert();
			RegexRunnerFactory result;
			try
			{
				result = regexLWCGCompiler.FactoryInstanceFromCode(code, options);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x00101020 File Offset: 0x000FF220
		internal static void CompileToAssembly(RegexCompilationInfo[] regexes, AssemblyName an, CustomAttributeBuilder[] attribs, string resourceFile)
		{
			RegexTypeCompiler regexTypeCompiler = new RegexTypeCompiler(an, attribs, resourceFile);
			for (int i = 0; i < regexes.Length; i++)
			{
				if (regexes[i] == null)
				{
					throw new ArgumentNullException("regexes", SR.GetString("ArgumentNull_ArrayWithNullElements"));
				}
				string pattern = regexes[i].Pattern;
				RegexOptions options = regexes[i].Options;
				string text;
				if (regexes[i].Namespace.Length == 0)
				{
					text = regexes[i].Name;
				}
				else
				{
					text = regexes[i].Namespace + "." + regexes[i].Name;
				}
				TimeSpan matchTimeout = regexes[i].MatchTimeout;
				RegexTree regexTree = RegexParser.Parse(pattern, options);
				RegexCode code = RegexWriter.Write(regexTree);
				new ReflectionPermission(PermissionState.Unrestricted).Assert();
				try
				{
					Type factory = regexTypeCompiler.FactoryTypeFromCode(code, options, text);
					regexTypeCompiler.GenerateRegexType(pattern, options, text, regexes[i].IsPublic, code, regexTree, factory, matchTimeout);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			regexTypeCompiler.Save();
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x0010111C File Offset: 0x000FF31C
		internal int AddBacktrackNote(int flags, Label l, int codepos)
		{
			if (this._notes == null || this._notecount >= this._notes.Length)
			{
				RegexCompiler.BacktrackNote[] array = new RegexCompiler.BacktrackNote[(this._notes == null) ? 16 : (this._notes.Length * 2)];
				if (this._notes != null)
				{
					Array.Copy(this._notes, 0, array, 0, this._notecount);
				}
				this._notes = array;
			}
			this._notes[this._notecount] = new RegexCompiler.BacktrackNote(flags, l, codepos);
			int notecount = this._notecount;
			this._notecount = notecount + 1;
			return notecount;
		}

		// Token: 0x06003E4A RID: 15946 RVA: 0x001011A6 File Offset: 0x000FF3A6
		internal int AddTrack()
		{
			return this.AddTrack(128);
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x001011B3 File Offset: 0x000FF3B3
		internal int AddTrack(int flags)
		{
			return this.AddBacktrackNote(flags, this.DefineLabel(), this._codepos);
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x001011C8 File Offset: 0x000FF3C8
		internal int AddGoto(int destpos)
		{
			if (this._goto[destpos] == -1)
			{
				this._goto[destpos] = this.AddBacktrackNote(0, this._labels[destpos], destpos);
			}
			return this._goto[destpos];
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x001011F9 File Offset: 0x000FF3F9
		internal int AddUniqueTrack(int i)
		{
			return this.AddUniqueTrack(i, 128);
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x00101207 File Offset: 0x000FF407
		internal int AddUniqueTrack(int i, int flags)
		{
			if (this._uniquenote[i] == -1)
			{
				this._uniquenote[i] = this.AddTrack(flags);
			}
			return this._uniquenote[i];
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x0010122B File Offset: 0x000FF42B
		internal Label DefineLabel()
		{
			return this._ilg.DefineLabel();
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x00101238 File Offset: 0x000FF438
		internal void MarkLabel(Label l)
		{
			this._ilg.MarkLabel(l);
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x00101246 File Offset: 0x000FF446
		internal int Operand(int i)
		{
			return this._codes[this._codepos + i + 1];
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x00101259 File Offset: 0x000FF459
		internal bool IsRtl()
		{
			return (this._regexopcode & 64) != 0;
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x00101267 File Offset: 0x000FF467
		internal bool IsCi()
		{
			return (this._regexopcode & 512) != 0;
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x00101278 File Offset: 0x000FF478
		internal int Code()
		{
			return this._regexopcode & 63;
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x00101283 File Offset: 0x000FF483
		internal void Ldstr(string str)
		{
			this._ilg.Emit(OpCodes.Ldstr, str);
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x00101296 File Offset: 0x000FF496
		internal void Ldc(int i)
		{
			if (i <= 127 && i >= -128)
			{
				this._ilg.Emit(OpCodes.Ldc_I4_S, (byte)i);
				return;
			}
			this._ilg.Emit(OpCodes.Ldc_I4, i);
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x001012C6 File Offset: 0x000FF4C6
		internal void LdcI8(long i)
		{
			if (i <= 2147483647L && i >= -2147483648L)
			{
				this.Ldc((int)i);
				this._ilg.Emit(OpCodes.Conv_I8);
				return;
			}
			this._ilg.Emit(OpCodes.Ldc_I8, i);
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x00101304 File Offset: 0x000FF504
		internal void Dup()
		{
			this._ilg.Emit(OpCodes.Dup);
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x00101316 File Offset: 0x000FF516
		internal void Ret()
		{
			this._ilg.Emit(OpCodes.Ret);
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x00101328 File Offset: 0x000FF528
		private void Rem()
		{
			this._ilg.Emit(OpCodes.Rem);
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x0010133A File Offset: 0x000FF53A
		private void Ceq()
		{
			this._ilg.Emit(OpCodes.Ceq);
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x0010134C File Offset: 0x000FF54C
		internal void Pop()
		{
			this._ilg.Emit(OpCodes.Pop);
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x0010135E File Offset: 0x000FF55E
		internal void Add()
		{
			this._ilg.Emit(OpCodes.Add);
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x00101370 File Offset: 0x000FF570
		internal void Add(bool negate)
		{
			if (negate)
			{
				this._ilg.Emit(OpCodes.Sub);
				return;
			}
			this._ilg.Emit(OpCodes.Add);
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x00101396 File Offset: 0x000FF596
		internal void Sub()
		{
			this._ilg.Emit(OpCodes.Sub);
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x001013A8 File Offset: 0x000FF5A8
		internal void Sub(bool negate)
		{
			if (negate)
			{
				this._ilg.Emit(OpCodes.Add);
				return;
			}
			this._ilg.Emit(OpCodes.Sub);
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x001013CE File Offset: 0x000FF5CE
		internal void Ldloc(LocalBuilder lt)
		{
			this._ilg.Emit(OpCodes.Ldloc_S, lt);
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x001013E1 File Offset: 0x000FF5E1
		internal void Stloc(LocalBuilder lt)
		{
			this._ilg.Emit(OpCodes.Stloc_S, lt);
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x001013F4 File Offset: 0x000FF5F4
		internal void Ldthis()
		{
			this._ilg.Emit(OpCodes.Ldarg_0);
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x00101406 File Offset: 0x000FF606
		internal void Ldthisfld(FieldInfo ft)
		{
			this.Ldthis();
			this._ilg.Emit(OpCodes.Ldfld, ft);
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x0010141F File Offset: 0x000FF61F
		internal void Mvfldloc(FieldInfo ft, LocalBuilder lt)
		{
			this.Ldthisfld(ft);
			this.Stloc(lt);
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x0010142F File Offset: 0x000FF62F
		internal void Mvlocfld(LocalBuilder lt, FieldInfo ft)
		{
			this.Ldthis();
			this.Ldloc(lt);
			this.Stfld(ft);
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x00101445 File Offset: 0x000FF645
		internal void Stfld(FieldInfo ft)
		{
			this._ilg.Emit(OpCodes.Stfld, ft);
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x00101458 File Offset: 0x000FF658
		internal void Callvirt(MethodInfo mt)
		{
			this._ilg.Emit(OpCodes.Callvirt, mt);
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x0010146B File Offset: 0x000FF66B
		internal void Call(MethodInfo mt)
		{
			this._ilg.Emit(OpCodes.Call, mt);
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x0010147E File Offset: 0x000FF67E
		internal void Newobj(ConstructorInfo ct)
		{
			this._ilg.Emit(OpCodes.Newobj, ct);
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x00101491 File Offset: 0x000FF691
		internal void BrfalseFar(Label l)
		{
			this._ilg.Emit(OpCodes.Brfalse, l);
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x001014A4 File Offset: 0x000FF6A4
		internal void BrtrueFar(Label l)
		{
			this._ilg.Emit(OpCodes.Brtrue, l);
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x001014B7 File Offset: 0x000FF6B7
		internal void BrFar(Label l)
		{
			this._ilg.Emit(OpCodes.Br, l);
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x001014CA File Offset: 0x000FF6CA
		internal void BleFar(Label l)
		{
			this._ilg.Emit(OpCodes.Ble, l);
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x001014DD File Offset: 0x000FF6DD
		internal void BltFar(Label l)
		{
			this._ilg.Emit(OpCodes.Blt, l);
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x001014F0 File Offset: 0x000FF6F0
		internal void BgeFar(Label l)
		{
			this._ilg.Emit(OpCodes.Bge, l);
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x00101503 File Offset: 0x000FF703
		internal void BgtFar(Label l)
		{
			this._ilg.Emit(OpCodes.Bgt, l);
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x00101516 File Offset: 0x000FF716
		internal void BneFar(Label l)
		{
			this._ilg.Emit(OpCodes.Bne_Un, l);
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x00101529 File Offset: 0x000FF729
		internal void BeqFar(Label l)
		{
			this._ilg.Emit(OpCodes.Beq, l);
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x0010153C File Offset: 0x000FF73C
		internal void Brfalse(Label l)
		{
			this._ilg.Emit(OpCodes.Brfalse_S, l);
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x0010154F File Offset: 0x000FF74F
		internal void Br(Label l)
		{
			this._ilg.Emit(OpCodes.Br_S, l);
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x00101562 File Offset: 0x000FF762
		internal void Ble(Label l)
		{
			this._ilg.Emit(OpCodes.Ble_S, l);
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x00101575 File Offset: 0x000FF775
		internal void Blt(Label l)
		{
			this._ilg.Emit(OpCodes.Blt_S, l);
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x00101588 File Offset: 0x000FF788
		internal void Bge(Label l)
		{
			this._ilg.Emit(OpCodes.Bge_S, l);
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x0010159B File Offset: 0x000FF79B
		internal void Bgt(Label l)
		{
			this._ilg.Emit(OpCodes.Bgt_S, l);
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x001015AE File Offset: 0x000FF7AE
		internal void Bgtun(Label l)
		{
			this._ilg.Emit(OpCodes.Bgt_Un_S, l);
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x001015C1 File Offset: 0x000FF7C1
		internal void Bne(Label l)
		{
			this._ilg.Emit(OpCodes.Bne_Un_S, l);
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x001015D4 File Offset: 0x000FF7D4
		internal void Beq(Label l)
		{
			this._ilg.Emit(OpCodes.Beq_S, l);
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x001015E7 File Offset: 0x000FF7E7
		internal void Ldlen()
		{
			this._ilg.Emit(OpCodes.Ldlen);
		}

		// Token: 0x06003E7E RID: 15998 RVA: 0x001015F9 File Offset: 0x000FF7F9
		internal void Rightchar()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Callvirt(RegexCompiler._getcharM);
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x00101620 File Offset: 0x000FF820
		internal void Rightcharnext()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Dup();
			this.Ldc(1);
			this.Add();
			this.Stloc(this._textposV);
			this.Callvirt(RegexCompiler._getcharM);
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x0010166F File Offset: 0x000FF86F
		internal void Leftchar()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub();
			this.Callvirt(RegexCompiler._getcharM);
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x001016A4 File Offset: 0x000FF8A4
		internal void Leftcharnext()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub();
			this.Dup();
			this.Stloc(this._textposV);
			this.Callvirt(RegexCompiler._getcharM);
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x001016F3 File Offset: 0x000FF8F3
		internal void Track()
		{
			this.ReadyPushTrack();
			this.Ldc(this.AddTrack());
			this.DoPush();
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x0010170D File Offset: 0x000FF90D
		internal void Trackagain()
		{
			this.ReadyPushTrack();
			this.Ldc(this._backpos);
			this.DoPush();
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x00101727 File Offset: 0x000FF927
		internal void PushTrack(LocalBuilder lt)
		{
			this.ReadyPushTrack();
			this.Ldloc(lt);
			this.DoPush();
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x0010173C File Offset: 0x000FF93C
		internal void TrackUnique(int i)
		{
			this.ReadyPushTrack();
			this.Ldc(this.AddUniqueTrack(i));
			this.DoPush();
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x00101757 File Offset: 0x000FF957
		internal void TrackUnique2(int i)
		{
			this.ReadyPushTrack();
			this.Ldc(this.AddUniqueTrack(i, 256));
			this.DoPush();
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x00101778 File Offset: 0x000FF978
		internal void ReadyPushTrack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Sub);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Stloc_S, this._trackposV);
		}

		// Token: 0x06003E88 RID: 16008 RVA: 0x001017F8 File Offset: 0x000FF9F8
		internal void PopTrack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Add);
			this._ilg.Emit(OpCodes.Stloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x00101887 File Offset: 0x000FFA87
		internal void TopTrack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x001018C5 File Offset: 0x000FFAC5
		internal void PushStack(LocalBuilder lt)
		{
			this.ReadyPushStack();
			this._ilg.Emit(OpCodes.Ldloc_S, lt);
			this.DoPush();
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x001018E4 File Offset: 0x000FFAE4
		internal void ReadyReplaceStack(int i)
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			if (i != 0)
			{
				this.Ldc(i);
				this._ilg.Emit(OpCodes.Add);
			}
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x00101938 File Offset: 0x000FFB38
		internal void ReadyPushStack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Sub);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Stloc_S, this._stackposV);
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x001019B7 File Offset: 0x000FFBB7
		internal void TopStack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x06003E8E RID: 16014 RVA: 0x001019F8 File Offset: 0x000FFBF8
		internal void PopStack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Add);
			this._ilg.Emit(OpCodes.Stloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x06003E8F RID: 16015 RVA: 0x00101A87 File Offset: 0x000FFC87
		internal void PopDiscardStack()
		{
			this.PopDiscardStack(1);
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x00101A90 File Offset: 0x000FFC90
		internal void PopDiscardStack(int i)
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this.Ldc(i);
			this._ilg.Emit(OpCodes.Add);
			this._ilg.Emit(OpCodes.Stloc_S, this._stackposV);
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x00101AE0 File Offset: 0x000FFCE0
		internal void DoReplace()
		{
			this._ilg.Emit(OpCodes.Stelem_I4);
		}

		// Token: 0x06003E92 RID: 16018 RVA: 0x00101AF2 File Offset: 0x000FFCF2
		internal void DoPush()
		{
			this._ilg.Emit(OpCodes.Stelem_I4);
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x00101B04 File Offset: 0x000FFD04
		internal void Back()
		{
			this._ilg.Emit(OpCodes.Br, this._backtrack);
		}

		// Token: 0x06003E94 RID: 16020 RVA: 0x00101B1C File Offset: 0x000FFD1C
		internal void Goto(int i)
		{
			if (i < this._codepos)
			{
				Label l = this.DefineLabel();
				this.Ldloc(this._trackposV);
				this.Ldc(this._trackcount * 4);
				this.Ble(l);
				this.Ldloc(this._stackposV);
				this.Ldc(this._trackcount * 3);
				this.BgtFar(this._labels[i]);
				this.MarkLabel(l);
				this.ReadyPushTrack();
				this.Ldc(this.AddGoto(i));
				this.DoPush();
				this.BrFar(this._backtrack);
				return;
			}
			this.BrFar(this._labels[i]);
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x00101BC8 File Offset: 0x000FFDC8
		internal int NextCodepos()
		{
			return this._codepos + RegexCode.OpcodeSize(this._codes[this._codepos]);
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x00101BE3 File Offset: 0x000FFDE3
		internal Label AdvanceLabel()
		{
			return this._labels[this.NextCodepos()];
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x00101BF6 File Offset: 0x000FFDF6
		internal void Advance()
		{
			this._ilg.Emit(OpCodes.Br, this.AdvanceLabel());
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x00101C0E File Offset: 0x000FFE0E
		internal void CallToLower()
		{
			if ((this._options & RegexOptions.CultureInvariant) != RegexOptions.None)
			{
				this.Call(RegexCompiler._getInvariantCulture);
			}
			else
			{
				this.Call(RegexCompiler._getCurrentCulture);
			}
			this.Call(RegexCompiler._chartolowerM);
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x00101C44 File Offset: 0x000FFE44
		internal void GenerateForwardSection()
		{
			this._labels = new Label[this._codes.Length];
			this._goto = new int[this._codes.Length];
			for (int i = 0; i < this._codes.Length; i += RegexCode.OpcodeSize(this._codes[i]))
			{
				this._goto[i] = -1;
				this._labels[i] = this._ilg.DefineLabel();
			}
			this._uniquenote = new int[10];
			for (int j = 0; j < 10; j++)
			{
				this._uniquenote[j] = -1;
			}
			this.Mvfldloc(RegexCompiler._textF, this._textV);
			this.Mvfldloc(RegexCompiler._textstartF, this._textstartV);
			this.Mvfldloc(RegexCompiler._textbegF, this._textbegV);
			this.Mvfldloc(RegexCompiler._textendF, this._textendV);
			this.Mvfldloc(RegexCompiler._textposF, this._textposV);
			this.Mvfldloc(RegexCompiler._trackF, this._trackV);
			this.Mvfldloc(RegexCompiler._trackposF, this._trackposV);
			this.Mvfldloc(RegexCompiler._stackF, this._stackV);
			this.Mvfldloc(RegexCompiler._stackposF, this._stackposV);
			this._backpos = -1;
			for (int i = 0; i < this._codes.Length; i += RegexCode.OpcodeSize(this._codes[i]))
			{
				this.MarkLabel(this._labels[i]);
				this._codepos = i;
				this._regexopcode = this._codes[i];
				this.GenerateOneCode();
			}
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x00101DC8 File Offset: 0x000FFFC8
		internal void GenerateMiddleSection()
		{
			Label label = this.DefineLabel();
			this.MarkLabel(this._backtrack);
			this.Mvlocfld(this._trackposV, RegexCompiler._trackposF);
			this.Mvlocfld(this._stackposV, RegexCompiler._stackposF);
			this.Ldthis();
			this.Callvirt(RegexCompiler._ensurestorageM);
			this.Mvfldloc(RegexCompiler._trackposF, this._trackposV);
			this.Mvfldloc(RegexCompiler._stackposF, this._stackposV);
			this.Mvfldloc(RegexCompiler._trackF, this._trackV);
			this.Mvfldloc(RegexCompiler._stackF, this._stackV);
			this.PopTrack();
			Label[] array = new Label[this._notecount];
			for (int i = 0; i < this._notecount; i++)
			{
				array[i] = this._notes[i]._label;
			}
			this._ilg.Emit(OpCodes.Switch, array);
		}

		// Token: 0x06003E9B RID: 16027 RVA: 0x00101EA8 File Offset: 0x001000A8
		internal void GenerateBacktrackSection()
		{
			for (int i = 0; i < this._notecount; i++)
			{
				RegexCompiler.BacktrackNote backtrackNote = this._notes[i];
				if (backtrackNote._flags != 0)
				{
					this._ilg.MarkLabel(backtrackNote._label);
					this._codepos = backtrackNote._codepos;
					this._backpos = i;
					this._regexopcode = (this._codes[backtrackNote._codepos] | backtrackNote._flags);
					this.GenerateOneCode();
				}
			}
		}

		// Token: 0x06003E9C RID: 16028 RVA: 0x00101F1C File Offset: 0x0010011C
		internal void GenerateFindFirstChar()
		{
			this._textposV = this.DeclareInt();
			this._textV = this.DeclareString();
			this._tempV = this.DeclareInt();
			this._temp2V = this.DeclareInt();
			if ((this._anchors & 53) != 0)
			{
				if (!this._code._rightToLeft)
				{
					if ((this._anchors & 1) != 0)
					{
						Label l = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textbegF);
						this.Ble(l);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textendF);
						this.Stfld(RegexCompiler._textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(l);
					}
					if ((this._anchors & 4) != 0)
					{
						Label l2 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textstartF);
						this.Ble(l2);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textendF);
						this.Stfld(RegexCompiler._textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(l2);
					}
					if ((this._anchors & 16) != 0)
					{
						Label l3 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textendF);
						this.Ldc(1);
						this.Sub();
						this.Bge(l3);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textendF);
						this.Ldc(1);
						this.Sub();
						this.Stfld(RegexCompiler._textposF);
						this.MarkLabel(l3);
					}
					if ((this._anchors & 32) != 0)
					{
						Label l4 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textendF);
						this.Bge(l4);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textendF);
						this.Stfld(RegexCompiler._textposF);
						this.MarkLabel(l4);
					}
				}
				else
				{
					if ((this._anchors & 32) != 0)
					{
						Label l5 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textendF);
						this.Bge(l5);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textbegF);
						this.Stfld(RegexCompiler._textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(l5);
					}
					if ((this._anchors & 16) != 0)
					{
						Label l6 = this.DefineLabel();
						Label l7 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textendF);
						this.Ldc(1);
						this.Sub();
						this.Blt(l6);
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textendF);
						this.Beq(l7);
						this.Ldthisfld(RegexCompiler._textF);
						this.Ldthisfld(RegexCompiler._textposF);
						this.Callvirt(RegexCompiler._getcharM);
						this.Ldc(10);
						this.Beq(l7);
						this.MarkLabel(l6);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textbegF);
						this.Stfld(RegexCompiler._textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(l7);
					}
					if ((this._anchors & 4) != 0)
					{
						Label l8 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textstartF);
						this.Bge(l8);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textbegF);
						this.Stfld(RegexCompiler._textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(l8);
					}
					if ((this._anchors & 1) != 0)
					{
						Label l9 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textbegF);
						this.Ble(l9);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textbegF);
						this.Stfld(RegexCompiler._textposF);
						this.MarkLabel(l9);
					}
				}
				this.Ldc(1);
				this.Ret();
				return;
			}
			if (this._bmPrefix != null && this._bmPrefix._negativeUnicode == null)
			{
				LocalBuilder tempV = this._tempV;
				LocalBuilder tempV2 = this._tempV;
				LocalBuilder temp2V = this._temp2V;
				Label label = this.DefineLabel();
				Label l10 = this.DefineLabel();
				Label l11 = this.DefineLabel();
				Label l12 = this.DefineLabel();
				Label label2 = this.DefineLabel();
				Label l13 = this.DefineLabel();
				int num;
				int index;
				if (!this._code._rightToLeft)
				{
					num = -1;
					index = this._bmPrefix._pattern.Length - 1;
				}
				else
				{
					num = this._bmPrefix._pattern.Length;
					index = 0;
				}
				int i = (int)this._bmPrefix._pattern[index];
				this.Mvfldloc(RegexCompiler._textF, this._textV);
				if (!this._code._rightToLeft)
				{
					this.Ldthisfld(RegexCompiler._textendF);
				}
				else
				{
					this.Ldthisfld(RegexCompiler._textbegF);
				}
				this.Stloc(temp2V);
				this.Ldthisfld(RegexCompiler._textposF);
				if (!this._code._rightToLeft)
				{
					this.Ldc(this._bmPrefix._pattern.Length - 1);
					this.Add();
				}
				else
				{
					this.Ldc(this._bmPrefix._pattern.Length);
					this.Sub();
				}
				this.Stloc(this._textposV);
				this.Br(l12);
				this.MarkLabel(label);
				if (!this._code._rightToLeft)
				{
					this.Ldc(this._bmPrefix._pattern.Length);
				}
				else
				{
					this.Ldc(-this._bmPrefix._pattern.Length);
				}
				this.MarkLabel(l10);
				this.Ldloc(this._textposV);
				this.Add();
				this.Stloc(this._textposV);
				this.MarkLabel(l12);
				this.Ldloc(this._textposV);
				this.Ldloc(temp2V);
				if (!this._code._rightToLeft)
				{
					this.BgeFar(l11);
				}
				else
				{
					this.BltFar(l11);
				}
				this.Rightchar();
				if (this._bmPrefix._caseInsensitive)
				{
					this.CallToLower();
				}
				this.Dup();
				this.Stloc(tempV);
				this.Ldc(i);
				this.BeqFar(l13);
				this.Ldloc(tempV);
				this.Ldc(this._bmPrefix._lowASCII);
				this.Sub();
				this.Dup();
				this.Stloc(tempV);
				this.Ldc(this._bmPrefix._highASCII - this._bmPrefix._lowASCII);
				this.Bgtun(label);
				Label[] array = new Label[this._bmPrefix._highASCII - this._bmPrefix._lowASCII + 1];
				for (int j = this._bmPrefix._lowASCII; j <= this._bmPrefix._highASCII; j++)
				{
					if (this._bmPrefix._negativeASCII[j] == num)
					{
						array[j - this._bmPrefix._lowASCII] = label;
					}
					else
					{
						array[j - this._bmPrefix._lowASCII] = this.DefineLabel();
					}
				}
				this.Ldloc(tempV);
				this._ilg.Emit(OpCodes.Switch, array);
				for (int j = this._bmPrefix._lowASCII; j <= this._bmPrefix._highASCII; j++)
				{
					if (this._bmPrefix._negativeASCII[j] != num)
					{
						this.MarkLabel(array[j - this._bmPrefix._lowASCII]);
						this.Ldc(this._bmPrefix._negativeASCII[j]);
						this.BrFar(l10);
					}
				}
				this.MarkLabel(l13);
				this.Ldloc(this._textposV);
				this.Stloc(tempV2);
				for (int j = this._bmPrefix._pattern.Length - 2; j >= 0; j--)
				{
					Label l14 = this.DefineLabel();
					int num2;
					if (!this._code._rightToLeft)
					{
						num2 = j;
					}
					else
					{
						num2 = this._bmPrefix._pattern.Length - 1 - j;
					}
					this.Ldloc(this._textV);
					this.Ldloc(tempV2);
					this.Ldc(1);
					this.Sub(this._code._rightToLeft);
					this.Dup();
					this.Stloc(tempV2);
					this.Callvirt(RegexCompiler._getcharM);
					if (this._bmPrefix._caseInsensitive)
					{
						this.CallToLower();
					}
					this.Ldc((int)this._bmPrefix._pattern[num2]);
					this.Beq(l14);
					this.Ldc(this._bmPrefix._positive[num2]);
					this.BrFar(l10);
					this.MarkLabel(l14);
				}
				this.Ldthis();
				this.Ldloc(tempV2);
				if (this._code._rightToLeft)
				{
					this.Ldc(1);
					this.Add();
				}
				this.Stfld(RegexCompiler._textposF);
				this.Ldc(1);
				this.Ret();
				this.MarkLabel(l11);
				this.Ldthis();
				if (!this._code._rightToLeft)
				{
					this.Ldthisfld(RegexCompiler._textendF);
				}
				else
				{
					this.Ldthisfld(RegexCompiler._textbegF);
				}
				this.Stfld(RegexCompiler._textposF);
				this.Ldc(0);
				this.Ret();
				return;
			}
			if (this._fcPrefix == null)
			{
				this.Ldc(1);
				this.Ret();
				return;
			}
			LocalBuilder temp2V2 = this._temp2V;
			LocalBuilder tempV3 = this._tempV;
			Label l15 = this.DefineLabel();
			Label l16 = this.DefineLabel();
			Label l17 = this.DefineLabel();
			Label l18 = this.DefineLabel();
			Label l19 = this.DefineLabel();
			this.Mvfldloc(RegexCompiler._textposF, this._textposV);
			this.Mvfldloc(RegexCompiler._textF, this._textV);
			if (!this._code._rightToLeft)
			{
				this.Ldthisfld(RegexCompiler._textendF);
				this.Ldloc(this._textposV);
			}
			else
			{
				this.Ldloc(this._textposV);
				this.Ldthisfld(RegexCompiler._textbegF);
			}
			this.Sub();
			this.Stloc(temp2V2);
			this.Ldloc(temp2V2);
			this.Ldc(0);
			this.BleFar(l18);
			this.MarkLabel(l15);
			this.Ldloc(temp2V2);
			this.Ldc(1);
			this.Sub();
			this.Stloc(temp2V2);
			if (this._code._rightToLeft)
			{
				this.Leftcharnext();
			}
			else
			{
				this.Rightcharnext();
			}
			if (this._fcPrefix.CaseInsensitive)
			{
				this.CallToLower();
			}
			if (!RegexCharClass.IsSingleton(this._fcPrefix.Prefix))
			{
				this.Ldstr(this._fcPrefix.Prefix);
				this.Call(RegexCompiler._charInSetM);
				this.BrtrueFar(l16);
			}
			else
			{
				this.Ldc((int)RegexCharClass.SingletonChar(this._fcPrefix.Prefix));
				this.Beq(l16);
			}
			this.MarkLabel(l19);
			this.Ldloc(temp2V2);
			this.Ldc(0);
			if (!RegexCharClass.IsSingleton(this._fcPrefix.Prefix))
			{
				this.BgtFar(l15);
			}
			else
			{
				this.Bgt(l15);
			}
			this.Ldc(0);
			this.BrFar(l17);
			this.MarkLabel(l16);
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub(this._code._rightToLeft);
			this.Stloc(this._textposV);
			this.Ldc(1);
			this.MarkLabel(l17);
			this.Mvlocfld(this._textposV, RegexCompiler._textposF);
			this.Ret();
			this.MarkLabel(l18);
			this.Ldc(0);
			this.Ret();
		}

		// Token: 0x06003E9D RID: 16029 RVA: 0x00102A5B File Offset: 0x00100C5B
		internal void GenerateInitTrackCount()
		{
			this.Ldthis();
			this.Ldc(this._trackcount);
			this.Stfld(RegexCompiler._trackcountF);
			this.Ret();
		}

		// Token: 0x06003E9E RID: 16030 RVA: 0x00102A80 File Offset: 0x00100C80
		internal LocalBuilder DeclareInt()
		{
			return this._ilg.DeclareLocal(typeof(int));
		}

		// Token: 0x06003E9F RID: 16031 RVA: 0x00102A97 File Offset: 0x00100C97
		internal LocalBuilder DeclareIntArray()
		{
			return this._ilg.DeclareLocal(typeof(int[]));
		}

		// Token: 0x06003EA0 RID: 16032 RVA: 0x00102AAE File Offset: 0x00100CAE
		internal LocalBuilder DeclareString()
		{
			return this._ilg.DeclareLocal(typeof(string));
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x00102AC8 File Offset: 0x00100CC8
		internal void GenerateGo()
		{
			this._textposV = this.DeclareInt();
			this._textV = this.DeclareString();
			this._trackposV = this.DeclareInt();
			this._trackV = this.DeclareIntArray();
			this._stackposV = this.DeclareInt();
			this._stackV = this.DeclareIntArray();
			this._tempV = this.DeclareInt();
			this._temp2V = this.DeclareInt();
			this._temp3V = this.DeclareInt();
			this._textbegV = this.DeclareInt();
			this._textendV = this.DeclareInt();
			this._textstartV = this.DeclareInt();
			if (!RegexCompiler.UseLegacyTimeoutCheck)
			{
				this._loopV = this.DeclareInt();
			}
			this._labels = null;
			this._notes = null;
			this._notecount = 0;
			this._backtrack = this.DefineLabel();
			this.GenerateForwardSection();
			this.GenerateMiddleSection();
			this.GenerateBacktrackSection();
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x00102BAC File Offset: 0x00100DAC
		internal void GenerateOneCode()
		{
			this.Ldthis();
			this.Callvirt(RegexCompiler._checkTimeoutM);
			int regexopcode = this._regexopcode;
			if (regexopcode <= 285)
			{
				if (regexopcode <= 164)
				{
					switch (regexopcode)
					{
					case 0:
					case 1:
					case 2:
					case 64:
					case 65:
					case 66:
						goto IL_143A;
					case 3:
					case 4:
					case 5:
					case 67:
					case 68:
					case 69:
						goto IL_1613;
					case 6:
					case 7:
					case 8:
					case 70:
					case 71:
					case 72:
						goto IL_190B;
					case 9:
					case 10:
					case 11:
					case 73:
					case 74:
					case 75:
						break;
					case 12:
						goto IL_1026;
					case 13:
					case 77:
						goto IL_11F8;
					case 14:
					{
						Label l = this._labels[this.NextCodepos()];
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.Ble(l);
						this.Leftchar();
						this.Ldc(10);
						this.BneFar(this._backtrack);
						return;
					}
					case 15:
					{
						Label l2 = this._labels[this.NextCodepos()];
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.Bge(l2);
						this.Rightchar();
						this.Ldc(10);
						this.BneFar(this._backtrack);
						return;
					}
					case 16:
					case 17:
						this.Ldthis();
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.Ldloc(this._textendV);
						this.Callvirt(RegexCompiler._isboundaryM);
						if (this.Code() == 16)
						{
							this.BrfalseFar(this._backtrack);
							return;
						}
						this.BrtrueFar(this._backtrack);
						return;
					case 18:
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.BgtFar(this._backtrack);
						return;
					case 19:
						this.Ldloc(this._textposV);
						this.Ldthisfld(RegexCompiler._textstartF);
						this.BneFar(this._backtrack);
						return;
					case 20:
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.Ldc(1);
						this.Sub();
						this.BltFar(this._backtrack);
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.Bge(this._labels[this.NextCodepos()]);
						this.Rightchar();
						this.Ldc(10);
						this.BneFar(this._backtrack);
						return;
					case 21:
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.BltFar(this._backtrack);
						return;
					case 22:
						this.Back();
						return;
					case 23:
						this.PushTrack(this._textposV);
						this.Track();
						return;
					case 24:
					{
						LocalBuilder tempV = this._tempV;
						Label l3 = this.DefineLabel();
						this.PopStack();
						this.Dup();
						this.Stloc(tempV);
						this.PushTrack(tempV);
						this.Ldloc(this._textposV);
						this.Beq(l3);
						this.PushTrack(this._textposV);
						this.PushStack(this._textposV);
						this.Track();
						this.Goto(this.Operand(0));
						this.MarkLabel(l3);
						this.TrackUnique2(5);
						return;
					}
					case 25:
					{
						LocalBuilder tempV2 = this._tempV;
						Label l4 = this.DefineLabel();
						Label l5 = this.DefineLabel();
						Label l6 = this.DefineLabel();
						this.PopStack();
						this.Dup();
						this.Stloc(tempV2);
						this.Ldloc(tempV2);
						this.Ldc(-1);
						this.Beq(l5);
						this.PushTrack(tempV2);
						this.Br(l6);
						this.MarkLabel(l5);
						this.PushTrack(this._textposV);
						this.MarkLabel(l6);
						this.Ldloc(this._textposV);
						this.Beq(l4);
						this.PushTrack(this._textposV);
						this.Track();
						this.Br(this.AdvanceLabel());
						this.MarkLabel(l4);
						this.ReadyPushStack();
						this.Ldloc(tempV2);
						this.DoPush();
						this.TrackUnique2(6);
						return;
					}
					case 26:
						this.ReadyPushStack();
						this.Ldc(-1);
						this.DoPush();
						this.ReadyPushStack();
						this.Ldc(this.Operand(0));
						this.DoPush();
						this.TrackUnique(1);
						return;
					case 27:
						this.PushStack(this._textposV);
						this.ReadyPushStack();
						this.Ldc(this.Operand(0));
						this.DoPush();
						this.TrackUnique(1);
						return;
					case 28:
					{
						LocalBuilder tempV3 = this._tempV;
						LocalBuilder temp2V = this._temp2V;
						Label l7 = this.DefineLabel();
						Label l8 = this.DefineLabel();
						this.PopStack();
						this.Stloc(tempV3);
						this.PopStack();
						this.Dup();
						this.Stloc(temp2V);
						this.PushTrack(temp2V);
						this.Ldloc(this._textposV);
						this.Bne(l7);
						this.Ldloc(tempV3);
						this.Ldc(0);
						this.Bge(l8);
						this.MarkLabel(l7);
						this.Ldloc(tempV3);
						this.Ldc(this.Operand(1));
						this.Bge(l8);
						this.PushStack(this._textposV);
						this.ReadyPushStack();
						this.Ldloc(tempV3);
						this.Ldc(1);
						this.Add();
						this.DoPush();
						this.Track();
						this.Goto(this.Operand(0));
						this.MarkLabel(l8);
						this.PushTrack(tempV3);
						this.TrackUnique2(7);
						return;
					}
					case 29:
					{
						LocalBuilder tempV4 = this._tempV;
						LocalBuilder temp2V2 = this._temp2V;
						Label l9 = this.DefineLabel();
						Label label = this.DefineLabel();
						Label label2 = this._labels[this.NextCodepos()];
						this.PopStack();
						this.Stloc(tempV4);
						this.PopStack();
						this.Stloc(temp2V2);
						this.Ldloc(tempV4);
						this.Ldc(0);
						this.Bge(l9);
						this.PushTrack(temp2V2);
						this.PushStack(this._textposV);
						this.ReadyPushStack();
						this.Ldloc(tempV4);
						this.Ldc(1);
						this.Add();
						this.DoPush();
						this.TrackUnique2(8);
						this.Goto(this.Operand(0));
						this.MarkLabel(l9);
						this.PushTrack(temp2V2);
						this.PushTrack(tempV4);
						this.PushTrack(this._textposV);
						this.Track();
						return;
					}
					case 30:
						this.ReadyPushStack();
						this.Ldc(-1);
						this.DoPush();
						this.TrackUnique(0);
						return;
					case 31:
						this.PushStack(this._textposV);
						this.TrackUnique(0);
						return;
					case 32:
						if (this.Operand(1) != -1)
						{
							this.Ldthis();
							this.Ldc(this.Operand(1));
							this.Callvirt(RegexCompiler._ismatchedM);
							this.BrfalseFar(this._backtrack);
						}
						this.PopStack();
						this.Stloc(this._tempV);
						if (this.Operand(1) != -1)
						{
							this.Ldthis();
							this.Ldc(this.Operand(0));
							this.Ldc(this.Operand(1));
							this.Ldloc(this._tempV);
							this.Ldloc(this._textposV);
							this.Callvirt(RegexCompiler._transferM);
						}
						else
						{
							this.Ldthis();
							this.Ldc(this.Operand(0));
							this.Ldloc(this._tempV);
							this.Ldloc(this._textposV);
							this.Callvirt(RegexCompiler._captureM);
						}
						this.PushTrack(this._tempV);
						if (this.Operand(0) != -1 && this.Operand(1) != -1)
						{
							this.TrackUnique(4);
							return;
						}
						this.TrackUnique(3);
						return;
					case 33:
						this.ReadyPushTrack();
						this.PopStack();
						this.Dup();
						this.Stloc(this._textposV);
						this.DoPush();
						this.Track();
						return;
					case 34:
						this.ReadyPushStack();
						this.Ldthisfld(RegexCompiler._trackF);
						this.Ldlen();
						this.Ldloc(this._trackposV);
						this.Sub();
						this.DoPush();
						this.ReadyPushStack();
						this.Ldthis();
						this.Callvirt(RegexCompiler._crawlposM);
						this.DoPush();
						this.TrackUnique(1);
						return;
					case 35:
					{
						Label l10 = this.DefineLabel();
						Label l11 = this.DefineLabel();
						this.PopStack();
						this.Ldthisfld(RegexCompiler._trackF);
						this.Ldlen();
						this.PopStack();
						this.Sub();
						this.Stloc(this._trackposV);
						this.Dup();
						this.Ldthis();
						this.Callvirt(RegexCompiler._crawlposM);
						this.Beq(l11);
						this.MarkLabel(l10);
						this.Ldthis();
						this.Callvirt(RegexCompiler._uncaptureM);
						this.Dup();
						this.Ldthis();
						this.Callvirt(RegexCompiler._crawlposM);
						this.Bne(l10);
						this.MarkLabel(l11);
						this.Pop();
						this.Back();
						return;
					}
					case 36:
						this.PopStack();
						this.Stloc(this._tempV);
						this.Ldthisfld(RegexCompiler._trackF);
						this.Ldlen();
						this.PopStack();
						this.Sub();
						this.Stloc(this._trackposV);
						this.PushTrack(this._tempV);
						this.TrackUnique(9);
						return;
					case 37:
						this.Ldthis();
						this.Ldc(this.Operand(0));
						this.Callvirt(RegexCompiler._ismatchedM);
						this.BrfalseFar(this._backtrack);
						return;
					case 38:
						this.Goto(this.Operand(0));
						return;
					case 39:
					case 43:
					case 44:
					case 45:
					case 46:
					case 47:
					case 48:
					case 49:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 56:
					case 57:
					case 58:
					case 59:
					case 60:
					case 61:
					case 62:
					case 63:
						goto IL_1B00;
					case 40:
						this.Mvlocfld(this._textposV, RegexCompiler._textposF);
						this.Ret();
						return;
					case 41:
					case 42:
						this.Ldthis();
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.Ldloc(this._textendV);
						this.Callvirt(RegexCompiler._isECMABoundaryM);
						if (this.Code() == 41)
						{
							this.BrfalseFar(this._backtrack);
							return;
						}
						this.BrtrueFar(this._backtrack);
						return;
					case 76:
						goto IL_110D;
					default:
						switch (regexopcode)
						{
						case 131:
						case 132:
						case 133:
							goto IL_186B;
						case 134:
						case 135:
						case 136:
							goto IL_19F5;
						case 137:
						case 138:
						case 139:
						case 140:
						case 141:
						case 142:
						case 143:
						case 144:
						case 145:
						case 146:
						case 147:
						case 148:
						case 149:
						case 150:
						case 163:
							goto IL_1B00;
						case 151:
							this.PopTrack();
							this.Stloc(this._textposV);
							this.Goto(this.Operand(0));
							return;
						case 152:
							this.PopTrack();
							this.Stloc(this._textposV);
							this.PopStack();
							this.Pop();
							this.TrackUnique2(5);
							this.Advance();
							return;
						case 153:
							this.PopTrack();
							this.Stloc(this._textposV);
							this.PushStack(this._textposV);
							this.TrackUnique2(6);
							this.Goto(this.Operand(0));
							return;
						case 154:
						case 155:
							this.PopDiscardStack(2);
							this.Back();
							return;
						case 156:
						{
							LocalBuilder tempV5 = this._tempV;
							Label l12 = this.DefineLabel();
							this.PopStack();
							this.Ldc(1);
							this.Sub();
							this.Dup();
							this.Stloc(tempV5);
							this.Ldc(0);
							this.Blt(l12);
							this.PopStack();
							this.Stloc(this._textposV);
							this.PushTrack(tempV5);
							this.TrackUnique2(7);
							this.Advance();
							this.MarkLabel(l12);
							this.ReadyReplaceStack(0);
							this.PopTrack();
							this.DoReplace();
							this.PushStack(tempV5);
							this.Back();
							return;
						}
						case 157:
						{
							Label l13 = this.DefineLabel();
							LocalBuilder tempV6 = this._tempV;
							this.PopTrack();
							this.Stloc(this._textposV);
							this.PopTrack();
							this.Dup();
							this.Stloc(tempV6);
							this.Ldc(this.Operand(1));
							this.Bge(l13);
							this.Ldloc(this._textposV);
							this.TopTrack();
							this.Beq(l13);
							this.PushStack(this._textposV);
							this.ReadyPushStack();
							this.Ldloc(tempV6);
							this.Ldc(1);
							this.Add();
							this.DoPush();
							this.TrackUnique2(8);
							this.Goto(this.Operand(0));
							this.MarkLabel(l13);
							this.ReadyPushStack();
							this.PopTrack();
							this.DoPush();
							this.PushStack(tempV6);
							this.Back();
							return;
						}
						case 158:
						case 159:
							this.PopDiscardStack();
							this.Back();
							return;
						case 160:
							this.ReadyPushStack();
							this.PopTrack();
							this.DoPush();
							this.Ldthis();
							this.Callvirt(RegexCompiler._uncaptureM);
							if (this.Operand(0) != -1 && this.Operand(1) != -1)
							{
								this.Ldthis();
								this.Callvirt(RegexCompiler._uncaptureM);
							}
							this.Back();
							return;
						case 161:
							this.ReadyPushStack();
							this.PopTrack();
							this.DoPush();
							this.Back();
							return;
						case 162:
							this.PopDiscardStack(2);
							this.Back();
							return;
						case 164:
						{
							Label l14 = this.DefineLabel();
							Label l15 = this.DefineLabel();
							this.PopTrack();
							this.Dup();
							this.Ldthis();
							this.Callvirt(RegexCompiler._crawlposM);
							this.Beq(l15);
							this.MarkLabel(l14);
							this.Ldthis();
							this.Callvirt(RegexCompiler._uncaptureM);
							this.Dup();
							this.Ldthis();
							this.Callvirt(RegexCompiler._crawlposM);
							this.Bne(l14);
							this.MarkLabel(l15);
							this.Pop();
							this.Back();
							return;
						}
						default:
							goto IL_1B00;
						}
						break;
					}
				}
				else
				{
					if (regexopcode - 195 <= 2)
					{
						goto IL_186B;
					}
					if (regexopcode - 198 <= 2)
					{
						goto IL_19F5;
					}
					switch (regexopcode)
					{
					case 280:
						this.ReadyPushStack();
						this.PopTrack();
						this.DoPush();
						this.Back();
						return;
					case 281:
						this.ReadyReplaceStack(0);
						this.PopTrack();
						this.DoReplace();
						this.Back();
						return;
					case 282:
					case 283:
						goto IL_1B00;
					case 284:
						this.PopTrack();
						this.Stloc(this._tempV);
						this.ReadyPushStack();
						this.PopTrack();
						this.DoPush();
						this.PushStack(this._tempV);
						this.Back();
						return;
					case 285:
						this.ReadyReplaceStack(1);
						this.PopTrack();
						this.DoReplace();
						this.ReadyReplaceStack(0);
						this.TopStack();
						this.Ldc(1);
						this.Sub();
						this.DoReplace();
						this.Back();
						return;
					default:
						goto IL_1B00;
					}
				}
			}
			else if (regexopcode <= 645)
			{
				switch (regexopcode)
				{
				case 512:
				case 513:
				case 514:
					goto IL_143A;
				case 515:
				case 516:
				case 517:
					goto IL_1613;
				case 518:
				case 519:
				case 520:
					goto IL_190B;
				case 521:
				case 522:
				case 523:
					break;
				case 524:
					goto IL_1026;
				case 525:
					goto IL_11F8;
				default:
					switch (regexopcode)
					{
					case 576:
					case 577:
					case 578:
						goto IL_143A;
					case 579:
					case 580:
					case 581:
						goto IL_1613;
					case 582:
					case 583:
					case 584:
						goto IL_190B;
					case 585:
					case 586:
					case 587:
						break;
					case 588:
						goto IL_110D;
					case 589:
						goto IL_11F8;
					default:
						if (regexopcode - 643 > 2)
						{
							goto IL_1B00;
						}
						goto IL_186B;
					}
					break;
				}
			}
			else
			{
				if (regexopcode - 646 <= 2)
				{
					goto IL_19F5;
				}
				if (regexopcode - 707 <= 2)
				{
					goto IL_186B;
				}
				if (regexopcode - 710 > 2)
				{
					goto IL_1B00;
				}
				goto IL_19F5;
			}
			this.Ldloc(this._textposV);
			if (!this.IsRtl())
			{
				this.Ldloc(this._textendV);
				this.BgeFar(this._backtrack);
				this.Rightcharnext();
			}
			else
			{
				this.Ldloc(this._textbegV);
				this.BleFar(this._backtrack);
				this.Leftcharnext();
			}
			if (this.IsCi())
			{
				this.CallToLower();
			}
			if (this.Code() == 11)
			{
				this.Ldstr(this._strings[this.Operand(0)]);
				this.Call(RegexCompiler._charInSetM);
				this.BrfalseFar(this._backtrack);
				return;
			}
			this.Ldc(this.Operand(0));
			if (this.Code() == 9)
			{
				this.BneFar(this._backtrack);
				return;
			}
			this.BeqFar(this._backtrack);
			return;
			IL_1026:
			string text = this._strings[this.Operand(0)];
			this.Ldc(text.Length);
			this.Ldloc(this._textendV);
			this.Ldloc(this._textposV);
			this.Sub();
			this.BgtFar(this._backtrack);
			for (int i = 0; i < text.Length; i++)
			{
				this.Ldloc(this._textV);
				this.Ldloc(this._textposV);
				if (i != 0)
				{
					this.Ldc(i);
					this.Add();
				}
				this.Callvirt(RegexCompiler._getcharM);
				if (this.IsCi())
				{
					this.CallToLower();
				}
				this.Ldc((int)text[i]);
				this.BneFar(this._backtrack);
			}
			this.Ldloc(this._textposV);
			this.Ldc(text.Length);
			this.Add();
			this.Stloc(this._textposV);
			return;
			IL_110D:
			string text2 = this._strings[this.Operand(0)];
			this.Ldc(text2.Length);
			this.Ldloc(this._textposV);
			this.Ldloc(this._textbegV);
			this.Sub();
			this.BgtFar(this._backtrack);
			int j = text2.Length;
			while (j > 0)
			{
				j--;
				this.Ldloc(this._textV);
				this.Ldloc(this._textposV);
				this.Ldc(text2.Length - j);
				this.Sub();
				this.Callvirt(RegexCompiler._getcharM);
				if (this.IsCi())
				{
					this.CallToLower();
				}
				this.Ldc((int)text2[j]);
				this.BneFar(this._backtrack);
			}
			this.Ldloc(this._textposV);
			this.Ldc(text2.Length);
			this.Sub();
			this.Stloc(this._textposV);
			return;
			IL_11F8:
			LocalBuilder tempV7 = this._tempV;
			LocalBuilder temp2V3 = this._temp2V;
			Label l16 = this.DefineLabel();
			this.Ldthis();
			this.Ldc(this.Operand(0));
			this.Callvirt(RegexCompiler._ismatchedM);
			if ((this._options & RegexOptions.ECMAScript) != RegexOptions.None)
			{
				this.Brfalse(this.AdvanceLabel());
			}
			else
			{
				this.BrfalseFar(this._backtrack);
			}
			this.Ldthis();
			this.Ldc(this.Operand(0));
			this.Callvirt(RegexCompiler._matchlengthM);
			this.Dup();
			this.Stloc(tempV7);
			if (!this.IsRtl())
			{
				this.Ldloc(this._textendV);
				this.Ldloc(this._textposV);
			}
			else
			{
				this.Ldloc(this._textposV);
				this.Ldloc(this._textbegV);
			}
			this.Sub();
			this.BgtFar(this._backtrack);
			this.Ldthis();
			this.Ldc(this.Operand(0));
			this.Callvirt(RegexCompiler._matchindexM);
			if (!this.IsRtl())
			{
				this.Ldloc(tempV7);
				this.Add(this.IsRtl());
			}
			this.Stloc(temp2V3);
			this.Ldloc(this._textposV);
			this.Ldloc(tempV7);
			this.Add(this.IsRtl());
			this.Stloc(this._textposV);
			this.MarkLabel(l16);
			this.Ldloc(tempV7);
			this.Ldc(0);
			this.Ble(this.AdvanceLabel());
			this.Ldloc(this._textV);
			this.Ldloc(temp2V3);
			this.Ldloc(tempV7);
			if (this.IsRtl())
			{
				this.Ldc(1);
				this.Sub();
				this.Dup();
				this.Stloc(tempV7);
			}
			this.Sub(this.IsRtl());
			this.Callvirt(RegexCompiler._getcharM);
			if (this.IsCi())
			{
				this.CallToLower();
			}
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldloc(tempV7);
			if (!this.IsRtl())
			{
				this.Dup();
				this.Ldc(1);
				this.Sub();
				this.Stloc(tempV7);
			}
			this.Sub(this.IsRtl());
			this.Callvirt(RegexCompiler._getcharM);
			if (this.IsCi())
			{
				this.CallToLower();
			}
			this.Beq(l16);
			this.Back();
			return;
			IL_143A:
			LocalBuilder tempV8 = this._tempV;
			Label l17 = this.DefineLabel();
			int num = this.Operand(1);
			if (num == 0)
			{
				return;
			}
			this.Ldc(num);
			if (!this.IsRtl())
			{
				this.Ldloc(this._textendV);
				this.Ldloc(this._textposV);
			}
			else
			{
				this.Ldloc(this._textposV);
				this.Ldloc(this._textbegV);
			}
			this.Sub();
			this.BgtFar(this._backtrack);
			this.Ldloc(this._textposV);
			this.Ldc(num);
			this.Add(this.IsRtl());
			this.Stloc(this._textposV);
			this.Ldc(num);
			this.Stloc(tempV8);
			this.MarkLabel(l17);
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldloc(tempV8);
			if (this.IsRtl())
			{
				this.Ldc(1);
				this.Sub();
				this.Dup();
				this.Stloc(tempV8);
				this.Add();
			}
			else
			{
				this.Dup();
				this.Ldc(1);
				this.Sub();
				this.Stloc(tempV8);
				this.Sub();
			}
			this.Callvirt(RegexCompiler._getcharM);
			if (this.IsCi())
			{
				this.CallToLower();
			}
			if (this.Code() == 2)
			{
				if (!RegexCompiler.UseLegacyTimeoutCheck)
				{
					this.EmitTimeoutCheck();
				}
				this.Ldstr(this._strings[this.Operand(0)]);
				this.Call(RegexCompiler._charInSetM);
				this.BrfalseFar(this._backtrack);
			}
			else
			{
				this.Ldc(this.Operand(0));
				if (this.Code() == 0)
				{
					this.BneFar(this._backtrack);
				}
				else
				{
					this.BeqFar(this._backtrack);
				}
			}
			this.Ldloc(tempV8);
			this.Ldc(0);
			if (this.Code() == 2)
			{
				this.BgtFar(l17);
				return;
			}
			this.Bgt(l17);
			return;
			IL_1613:
			LocalBuilder tempV9 = this._tempV;
			LocalBuilder temp2V4 = this._temp2V;
			Label l18 = this.DefineLabel();
			Label l19 = this.DefineLabel();
			int num2 = this.Operand(1);
			if (num2 != 0)
			{
				if (!this.IsRtl())
				{
					this.Ldloc(this._textendV);
					this.Ldloc(this._textposV);
				}
				else
				{
					this.Ldloc(this._textposV);
					this.Ldloc(this._textbegV);
				}
				this.Sub();
				if (num2 != 2147483647)
				{
					Label l20 = this.DefineLabel();
					this.Dup();
					this.Ldc(num2);
					this.Blt(l20);
					this.Pop();
					this.Ldc(num2);
					this.MarkLabel(l20);
				}
				this.Dup();
				this.Stloc(temp2V4);
				this.Ldc(1);
				this.Add();
				this.Stloc(tempV9);
				this.MarkLabel(l18);
				this.Ldloc(tempV9);
				this.Ldc(1);
				this.Sub();
				this.Dup();
				this.Stloc(tempV9);
				this.Ldc(0);
				if (this.Code() == 5)
				{
					this.BleFar(l19);
				}
				else
				{
					this.Ble(l19);
				}
				if (this.IsRtl())
				{
					this.Leftcharnext();
				}
				else
				{
					this.Rightcharnext();
				}
				if (this.IsCi())
				{
					this.CallToLower();
				}
				if (this.Code() == 5)
				{
					if (!RegexCompiler.UseLegacyTimeoutCheck)
					{
						this.EmitTimeoutCheck();
					}
					this.Ldstr(this._strings[this.Operand(0)]);
					this.Call(RegexCompiler._charInSetM);
					this.BrtrueFar(l18);
				}
				else
				{
					this.Ldc(this.Operand(0));
					if (this.Code() == 3)
					{
						this.Beq(l18);
					}
					else
					{
						this.Bne(l18);
					}
				}
				this.Ldloc(this._textposV);
				this.Ldc(1);
				this.Sub(this.IsRtl());
				this.Stloc(this._textposV);
				this.MarkLabel(l19);
				this.Ldloc(temp2V4);
				this.Ldloc(tempV9);
				this.Ble(this.AdvanceLabel());
				this.ReadyPushTrack();
				this.Ldloc(temp2V4);
				this.Ldloc(tempV9);
				this.Sub();
				this.Ldc(1);
				this.Sub();
				this.DoPush();
				this.ReadyPushTrack();
				this.Ldloc(this._textposV);
				this.Ldc(1);
				this.Sub(this.IsRtl());
				this.DoPush();
				this.Track();
				return;
			}
			return;
			IL_186B:
			this.PopTrack();
			this.Stloc(this._textposV);
			this.PopTrack();
			this.Stloc(this._tempV);
			this.Ldloc(this._tempV);
			this.Ldc(0);
			this.BleFar(this.AdvanceLabel());
			this.ReadyPushTrack();
			this.Ldloc(this._tempV);
			this.Ldc(1);
			this.Sub();
			this.DoPush();
			this.ReadyPushTrack();
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub(this.IsRtl());
			this.DoPush();
			this.Trackagain();
			this.Advance();
			return;
			IL_190B:
			LocalBuilder tempV10 = this._tempV;
			int num3 = this.Operand(1);
			if (num3 != 0)
			{
				if (!this.IsRtl())
				{
					this.Ldloc(this._textendV);
					this.Ldloc(this._textposV);
				}
				else
				{
					this.Ldloc(this._textposV);
					this.Ldloc(this._textbegV);
				}
				this.Sub();
				if (num3 != 2147483647)
				{
					Label l21 = this.DefineLabel();
					this.Dup();
					this.Ldc(num3);
					this.Blt(l21);
					this.Pop();
					this.Ldc(num3);
					this.MarkLabel(l21);
				}
				this.Dup();
				this.Stloc(tempV10);
				this.Ldc(0);
				this.Ble(this.AdvanceLabel());
				this.ReadyPushTrack();
				this.Ldloc(tempV10);
				this.Ldc(1);
				this.Sub();
				this.DoPush();
				this.PushTrack(this._textposV);
				this.Track();
				return;
			}
			return;
			IL_19F5:
			this.PopTrack();
			this.Stloc(this._textposV);
			this.PopTrack();
			this.Stloc(this._temp2V);
			if (!this.IsRtl())
			{
				this.Rightcharnext();
			}
			else
			{
				this.Leftcharnext();
			}
			if (this.IsCi())
			{
				this.CallToLower();
			}
			if (this.Code() == 8)
			{
				this.Ldstr(this._strings[this.Operand(0)]);
				this.Call(RegexCompiler._charInSetM);
				this.BrfalseFar(this._backtrack);
			}
			else
			{
				this.Ldc(this.Operand(0));
				if (this.Code() == 6)
				{
					this.BneFar(this._backtrack);
				}
				else
				{
					this.BeqFar(this._backtrack);
				}
			}
			this.Ldloc(this._temp2V);
			this.Ldc(0);
			this.BleFar(this.AdvanceLabel());
			this.ReadyPushTrack();
			this.Ldloc(this._temp2V);
			this.Ldc(1);
			this.Sub();
			this.DoPush();
			this.PushTrack(this._textposV);
			this.Trackagain();
			this.Advance();
			return;
			IL_1B00:
			throw new NotImplementedException(SR.GetString("UnimplementedState"));
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x001046CC File Offset: 0x001028CC
		private void EmitTimeoutCheck()
		{
			Label l = this.DefineLabel();
			this.Ldloc(this._loopV);
			this.Ldc(1);
			this.Add();
			this.Stloc(this._loopV);
			this.Ldloc(this._loopV);
			this.Ldc(2000);
			this.Rem();
			this.Ldc(0);
			this.Ceq();
			this.Brfalse(l);
			this.Ldthis();
			this.Callvirt(RegexCompiler._checkTimeoutM);
			this.MarkLabel(l);
		}

		// Token: 0x04002D79 RID: 11641
		internal static FieldInfo _textbegF;

		// Token: 0x04002D7A RID: 11642
		internal static FieldInfo _textendF;

		// Token: 0x04002D7B RID: 11643
		internal static FieldInfo _textstartF;

		// Token: 0x04002D7C RID: 11644
		internal static FieldInfo _textposF;

		// Token: 0x04002D7D RID: 11645
		internal static FieldInfo _textF;

		// Token: 0x04002D7E RID: 11646
		internal static FieldInfo _trackposF;

		// Token: 0x04002D7F RID: 11647
		internal static FieldInfo _trackF;

		// Token: 0x04002D80 RID: 11648
		internal static FieldInfo _stackposF;

		// Token: 0x04002D81 RID: 11649
		internal static FieldInfo _stackF;

		// Token: 0x04002D82 RID: 11650
		internal static FieldInfo _trackcountF;

		// Token: 0x04002D83 RID: 11651
		internal static MethodInfo _ensurestorageM;

		// Token: 0x04002D84 RID: 11652
		internal static MethodInfo _captureM;

		// Token: 0x04002D85 RID: 11653
		internal static MethodInfo _transferM;

		// Token: 0x04002D86 RID: 11654
		internal static MethodInfo _uncaptureM;

		// Token: 0x04002D87 RID: 11655
		internal static MethodInfo _ismatchedM;

		// Token: 0x04002D88 RID: 11656
		internal static MethodInfo _matchlengthM;

		// Token: 0x04002D89 RID: 11657
		internal static MethodInfo _matchindexM;

		// Token: 0x04002D8A RID: 11658
		internal static MethodInfo _isboundaryM;

		// Token: 0x04002D8B RID: 11659
		internal static MethodInfo _isECMABoundaryM;

		// Token: 0x04002D8C RID: 11660
		internal static MethodInfo _chartolowerM;

		// Token: 0x04002D8D RID: 11661
		internal static MethodInfo _getcharM;

		// Token: 0x04002D8E RID: 11662
		internal static MethodInfo _crawlposM;

		// Token: 0x04002D8F RID: 11663
		internal static MethodInfo _charInSetM;

		// Token: 0x04002D90 RID: 11664
		internal static MethodInfo _getCurrentCulture;

		// Token: 0x04002D91 RID: 11665
		internal static MethodInfo _getInvariantCulture;

		// Token: 0x04002D92 RID: 11666
		internal static MethodInfo _checkTimeoutM;

		// Token: 0x04002D93 RID: 11667
		internal ILGenerator _ilg;

		// Token: 0x04002D94 RID: 11668
		internal LocalBuilder _textstartV;

		// Token: 0x04002D95 RID: 11669
		internal LocalBuilder _textbegV;

		// Token: 0x04002D96 RID: 11670
		internal LocalBuilder _textendV;

		// Token: 0x04002D97 RID: 11671
		internal LocalBuilder _textposV;

		// Token: 0x04002D98 RID: 11672
		internal LocalBuilder _textV;

		// Token: 0x04002D99 RID: 11673
		internal LocalBuilder _trackposV;

		// Token: 0x04002D9A RID: 11674
		internal LocalBuilder _trackV;

		// Token: 0x04002D9B RID: 11675
		internal LocalBuilder _stackposV;

		// Token: 0x04002D9C RID: 11676
		internal LocalBuilder _stackV;

		// Token: 0x04002D9D RID: 11677
		internal LocalBuilder _tempV;

		// Token: 0x04002D9E RID: 11678
		internal LocalBuilder _temp2V;

		// Token: 0x04002D9F RID: 11679
		internal LocalBuilder _temp3V;

		// Token: 0x04002DA0 RID: 11680
		internal LocalBuilder _loopV;

		// Token: 0x04002DA1 RID: 11681
		internal RegexCode _code;

		// Token: 0x04002DA2 RID: 11682
		internal int[] _codes;

		// Token: 0x04002DA3 RID: 11683
		internal string[] _strings;

		// Token: 0x04002DA4 RID: 11684
		internal RegexPrefix _fcPrefix;

		// Token: 0x04002DA5 RID: 11685
		internal RegexBoyerMoore _bmPrefix;

		// Token: 0x04002DA6 RID: 11686
		internal int _anchors;

		// Token: 0x04002DA7 RID: 11687
		internal Label[] _labels;

		// Token: 0x04002DA8 RID: 11688
		internal RegexCompiler.BacktrackNote[] _notes;

		// Token: 0x04002DA9 RID: 11689
		internal int _notecount;

		// Token: 0x04002DAA RID: 11690
		internal int _trackcount;

		// Token: 0x04002DAB RID: 11691
		internal Label _backtrack;

		// Token: 0x04002DAC RID: 11692
		internal int _regexopcode;

		// Token: 0x04002DAD RID: 11693
		internal int _codepos;

		// Token: 0x04002DAE RID: 11694
		internal int _backpos;

		// Token: 0x04002DAF RID: 11695
		internal RegexOptions _options;

		// Token: 0x04002DB0 RID: 11696
		internal int[] _uniquenote;

		// Token: 0x04002DB1 RID: 11697
		internal int[] _goto;

		// Token: 0x04002DB2 RID: 11698
		internal const int stackpop = 0;

		// Token: 0x04002DB3 RID: 11699
		internal const int stackpop2 = 1;

		// Token: 0x04002DB4 RID: 11700
		internal const int stackpop3 = 2;

		// Token: 0x04002DB5 RID: 11701
		internal const int capback = 3;

		// Token: 0x04002DB6 RID: 11702
		internal const int capback2 = 4;

		// Token: 0x04002DB7 RID: 11703
		internal const int branchmarkback2 = 5;

		// Token: 0x04002DB8 RID: 11704
		internal const int lazybranchmarkback2 = 6;

		// Token: 0x04002DB9 RID: 11705
		internal const int branchcountback2 = 7;

		// Token: 0x04002DBA RID: 11706
		internal const int lazybranchcountback2 = 8;

		// Token: 0x04002DBB RID: 11707
		internal const int forejumpback = 9;

		// Token: 0x04002DBC RID: 11708
		internal const int uniquecount = 10;

		// Token: 0x04002DBD RID: 11709
		private const int LoopTimeoutCheckCount = 2000;

		// Token: 0x04002DBE RID: 11710
		private static readonly bool UseLegacyTimeoutCheck = LocalAppContextSwitches.UseLegacyTimeoutCheck;

		// Token: 0x020008B8 RID: 2232
		internal sealed class BacktrackNote
		{
			// Token: 0x06004639 RID: 17977 RVA: 0x001254D0 File Offset: 0x001236D0
			internal BacktrackNote(int flags, Label label, int codepos)
			{
				this._codepos = codepos;
				this._flags = flags;
				this._label = label;
			}

			// Token: 0x04003B69 RID: 15209
			internal int _codepos;

			// Token: 0x04003B6A RID: 15210
			internal int _flags;

			// Token: 0x04003B6B RID: 15211
			internal Label _label;
		}
	}
}
