using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using OracleInternal.Common;

namespace OracleInternal.SelfTuning
{
	// Token: 0x02000197 RID: 407
	[SecurityPermission(SecurityAction.Assert, ControlThread = true)]
	internal sealed class OracleTuner : OracleTunerBase, IOracleTuner
	{
		// Token: 0x06000F64 RID: 3940 RVA: 0x000A0640 File Offset: 0x0009E840
		private OracleTuner()
		{
			try
			{
				ulong num = 0UL;
				ulong num2 = 0UL;
				this.m_isUsableMemInfoAvail = OracleTuner.SystemInfo.getTotalVirtualAndPhysicalMemory(ref num, ref num2);
				if (this.m_isUsableMemInfoAvail)
				{
					this.m_installedRAM = num2;
					this.m_availableVM = ((num2 < num) ? num2 : num);
					this.m_minRAMNeeded = (ulong)(0.2f * this.m_installedRAM);
					this.m_minRAMReqdForTuning = (ulong)(0.3f * this.m_installedRAM);
					this.m_highMem = (ulong)(0.7f * this.m_availableVM);
					this.m_veryHighMem = (ulong)(0.8f * this.m_availableVM);
				}
				else if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)268500992, new string[]
					{
						"OracleTuningAgent::ctor(): Memory information not available"
					});
				}
			}
			catch (Exception ex)
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)268500992, new string[]
					{
						"OracleTuningAgent::ctor(): " + ex.Message
					});
				}
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x000A0768 File Offset: 0x0009E968
		public bool HighMemoryUsageAlert
		{
			get
			{
				return this.bHighMemoryAlertFlag;
			}
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x000A0770 File Offset: 0x0009E970
		public void setThreshold(IOracleTunable tunable, int val)
		{
			if (tunable != null && this.m_input.Keys.Contains(tunable.ID))
			{
				this.m_input[tunable.ID].MaxAllowedCursors = val;
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.SelfTuning, new string[]
				{
					string.Concat(new object[]
					{
						"OracleTuningAgent::setThreshold(): Max open-cursors count for pool ",
						tunable.ID,
						" is ",
						val
					})
				});
			}
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x000A07FC File Offset: 0x0009E9FC
		public bool Register(IOracleTunable tunable)
		{
			if (!this.m_isUsableMemInfoAvail || tunable == null || string.IsNullOrWhiteSpace(tunable.ID))
			{
				return false;
			}
			lock (this.m_registrationLock)
			{
				OracleTuner.OracleTunerInput oracleTunerInput = null;
				bool flag2 = false;
				lock (this.m_input)
				{
					flag2 = (this.m_input.Count == 0);
					if (this.m_input.TryGetValue(tunable.ID, out oracleTunerInput))
					{
						if (oracleTunerInput != null)
						{
							oracleTunerInput.m_registered = true;
							oracleTunerInput.m_UpdateRecommendationsDelegate = new Action<RecommendationType, int>(tunable.OnUpdateRecommendations);
						}
					}
					else
					{
						oracleTunerInput = new OracleTuner.OracleTunerInput
						{
							m_ID = tunable.ID,
							m_UpdateRecommendationsDelegate = new Action<RecommendationType, int>(tunable.OnUpdateRecommendations)
						};
						this.m_input.Add(tunable.ID, oracleTunerInput);
					}
				}
				if (!this.m_tunerEvt.IsSet)
				{
					this.m_tunerEvt.Set();
				}
				if (flag2 && this.m_tuningThread == null)
				{
					this.startTunerThread();
				}
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.SelfTuning, new string[]
				{
					"OracleTuningAgent::Register(): Registered pool " + tunable.ID
				});
			}
			return true;
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x000A095C File Offset: 0x0009EB5C
		public bool Unregister(IOracleTunable tunable)
		{
			if (!this.m_isUsableMemInfoAvail || tunable == null || string.IsNullOrWhiteSpace(tunable.ID))
			{
				return false;
			}
			lock (this.m_registrationLock)
			{
				OracleTuner.OracleTunerInput oracleTunerInput = null;
				bool flag2 = false;
				lock (this.m_input)
				{
					if (this.m_input.TryGetValue(tunable.ID, out oracleTunerInput) && oracleTunerInput != null)
					{
						oracleTunerInput.m_registered = false;
						oracleTunerInput.m_UpdateRecommendationsDelegate = null;
						if (tunable.ID != null)
						{
							this.m_input.Remove(tunable.ID);
						}
					}
					flag2 = !this.m_input.Any((KeyValuePair<string, OracleTuner.OracleTunerInput> ai) => ai.Value != null && ai.Value.m_registered);
				}
				if (flag2 && this.m_tunerEvt.IsSet)
				{
					this.m_tunerEvt.Reset();
				}
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.SelfTuning, new string[]
				{
					"OracleTuningAgent::Unegister(): Unegistered pool " + tunable.ID
				});
			}
			return true;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x000A0A9C File Offset: 0x0009EC9C
		public bool SubmitData(IOracleTunable tunable, int scs, int connCount, Dictionary<string, int> sample)
		{
			if (!this.m_isUsableMemInfoAvail || tunable == null || string.IsNullOrWhiteSpace(tunable.ID) || this.bHighMemoryAlertFlag)
			{
				return false;
			}
			OracleTuner.OracleTunerInput oracleTunerInput = null;
			if (!this.m_input.TryGetValue(tunable.ID, out oracleTunerInput) || oracleTunerInput == null || !oracleTunerInput.m_registered)
			{
				return false;
			}
			lock (oracleTunerInput)
			{
				if (oracleTunerInput.m_listOfData.Count >= 10)
				{
					oracleTunerInput.m_listOfData.RemoveAt(0);
				}
				oracleTunerInput.m_scs = scs;
				oracleTunerInput.m_noOfConnections = connCount;
				oracleTunerInput.m_listOfData.Add(sample);
				oracleTunerInput.m_noOfSubmissions++;
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.SelfTuning, new string[]
				{
					"OracleTuningAgent::SubmitData(): Submission done by " + tunable.ID
				});
			}
			return true;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x000A0B8C File Offset: 0x0009ED8C
		protected override void TuningFunction()
		{
			try
			{
				base.TuningFunction();
			}
			catch (Exception ex)
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)268500992, new string[]
					{
						"OracleTuningAgent::TuningFunction(): " + ex.Message
					});
				}
				this.m_selectedInput = null;
				this.m_agentState = OracleTunerState.SCAN;
			}
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x000A0BF0 File Offset: 0x0009EDF0
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		protected override void DoScan()
		{
			if (this.m_scanCyclesToSkip > 0)
			{
				Thread.Sleep(10000 * this.m_scanCyclesToSkip);
			}
			ulong currentProcessVirtualMemoryUsage = (ulong)OracleTuner.SystemInfo.CurrentProcessVirtualMemoryUsage;
			ulong availablePhysicalMemory = OracleTuner.SystemInfo.getAvailablePhysicalMemory();
			if (availablePhysicalMemory <= 0UL || currentProcessVirtualMemoryUsage <= 0UL)
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)268500992, new string[]
					{
						"OracleTuningAgent::DoScan(): Available memory information not found."
					});
				}
				return;
			}
			if (currentProcessVirtualMemoryUsage < this.m_highMem && availablePhysicalMemory > this.m_minRAMReqdForTuning)
			{
				this.bHighMemoryAlertFlag = false;
				this.SelectPoolToOptimize();
				if (this.m_selectedInput != null)
				{
					this.m_agentState = OracleTunerState.OPTIMIZE;
					return;
				}
				if (this.m_scanCyclesToSkip > 0)
				{
					Thread.Sleep(10000 * this.m_scanCyclesToSkip);
				}
				this.m_scanCyclesToSkip = 1;
				this.m_agentState = OracleTunerState.SCAN;
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.SelfTuning, new string[]
					{
						"OracleTuningAgent::DoScan(): No pools identified for optimization. Stay in SCAN state."
					});
					return;
				}
			}
			else
			{
				if (currentProcessVirtualMemoryUsage > this.m_veryHighMem || availablePhysicalMemory < this.m_minRAMNeeded)
				{
					this.m_memoryConsumptionToReduce = 0UL;
					if (currentProcessVirtualMemoryUsage > this.m_veryHighMem)
					{
						this.m_memoryConsumptionToReduce = currentProcessVirtualMemoryUsage - this.m_veryHighMem;
					}
					if (availablePhysicalMemory < this.m_minRAMNeeded && this.m_memoryConsumptionToReduce < this.m_minRAMNeeded - availablePhysicalMemory)
					{
						this.m_memoryConsumptionToReduce = this.m_minRAMNeeded - availablePhysicalMemory;
					}
					if (!this.bHighMemoryAlertFlag)
					{
						this.ReleaseSamplesFromAllAgentInputs();
					}
					this.bHighMemoryAlertFlag = true;
					this.m_agentState = OracleTunerState.REDUCE;
					return;
				}
				if (!this.bHighMemoryAlertFlag)
				{
					this.ReleaseSamplesFromAllAgentInputs();
				}
				this.bHighMemoryAlertFlag = true;
				this.m_scanCyclesToSkip = 1;
				this.m_agentState = OracleTunerState.SCAN;
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.SelfTuning, new string[]
					{
						"OracleTuningAgent::DoScan(): High memory usage. Stay in SCAN state."
					});
				}
			}
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x000A0D8C File Offset: 0x0009EF8C
		protected override void DoWatch()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)65792, new string[0]);
			}
			if (this.m_watchCyclesToSkip > 0)
			{
				Thread.Sleep(10000);
			}
			ulong currentProcessVirtualMemoryUsage = (ulong)OracleTuner.SystemInfo.CurrentProcessVirtualMemoryUsage;
			ulong availablePhysicalMemory = OracleTuner.SystemInfo.getAvailablePhysicalMemory();
			if (availablePhysicalMemory <= 0UL || currentProcessVirtualMemoryUsage <= 0UL)
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)268500992, new string[]
					{
						"OracleTuningAgent::DoWatch(): Available memory information not found."
					});
				}
				this.m_agentState = OracleTunerState.REVERT;
				this.m_watchCyclesToSkip = 0;
				return;
			}
			if (currentProcessVirtualMemoryUsage > this.m_veryHighMem || availablePhysicalMemory < this.m_minRAMNeeded)
			{
				this.m_agentState = OracleTunerState.REVERT;
				this.m_watchCyclesToSkip = 0;
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.SelfTuning, new string[]
					{
						"OracleTuningAgent::DoWatch(): High memory usage. Attempt REVERT"
					});
				}
			}
			else if (--this.m_watchCyclesToSkip > 0)
			{
				this.m_agentState = OracleTunerState.WATCH;
			}
			else
			{
				this.m_agentState = OracleTunerState.SCAN;
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)66048, new string[0]);
			}
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x000A0E90 File Offset: 0x0009F090
		protected override void DoWait()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)65792, new string[0]);
			}
			int num = 0;
			int num2;
			do
			{
				Thread.Sleep(5000);
				int count = this.m_input.Count;
				num2 = count - num;
				num = count;
			}
			while (num2 <= 0);
			Thread.Sleep(5000);
			base.DoWait();
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)66048, new string[0]);
			}
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x000A0F04 File Offset: 0x0009F104
		protected override void DoRevert()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)65792, new string[0]);
			}
			try
			{
				this.m_selectedInput.m_RecommendedSCS = this.m_selectedInput.m_scs;
			}
			catch (Exception ex)
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)268500992, new string[]
					{
						"OracleTuningAgent::DoRevert()" + ex.Message
					});
				}
			}
			this.m_scanCyclesToSkip = 6;
			base.DoRevert();
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)66048, new string[0]);
			}
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x000A0FA8 File Offset: 0x0009F1A8
		protected override void DoReduce()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)65792, new string[0]);
			}
			IEnumerable<OracleTuner.OracleTunerInput> enumerable = from ai in this.m_input.Values
			where ai.m_registered && ai.m_RecommendedSCS > 30
			select ai;
			if (!enumerable.Any<OracleTuner.OracleTunerInput>())
			{
				this.m_scanCyclesToSkip = 3;
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.SelfTuning, new string[]
					{
						"OracleTuningAgent::DoReduce(): No pools eligible for reduction. Delay SCAN by 30 secs"
					});
				}
			}
			else
			{
				int num = (int)Math.Ceiling(this.m_memoryConsumptionToReduce / 51200.0);
				int num2 = 0;
				foreach (OracleTuner.OracleTunerInput oracleTunerInput in enumerable)
				{
					num2 += oracleTunerInput.m_noOfConnections;
				}
				int num3 = (int)Math.Ceiling((double)num / (double)num2);
				foreach (OracleTuner.OracleTunerInput oracleTunerInput2 in enumerable)
				{
					int num4 = oracleTunerInput2.m_RecommendedSCS - num3;
					oracleTunerInput2.m_RecommendedSCS = ((num4 < 30) ? 30 : num4);
				}
				this.m_memoryConsumptionToReduce = 0UL;
				this.m_scanCyclesToSkip = 6;
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.SelfTuning, new string[]
					{
						"OracleTuningAgent::DoReduce(): SCS reduction recommended is " + num3 + ". Delay SCAN by 60 secs"
					});
				}
			}
			base.DoReduce();
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)66048, new string[0]);
			}
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x000A115C File Offset: 0x0009F35C
		protected override void DoOptimize()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)65792, new string[0]);
			}
			if (this.m_selectedInput.m_scs < OracleTuner.m_MaxStatementCacheSize && this.m_selectedInput.m_scs < this.m_selectedInput.MaxAllowedCursors)
			{
				Dictionary<string, int> collatedData = this.m_selectedInput.m_collatedData;
				if (collatedData == null || collatedData.Count <= 0)
				{
					return;
				}
				if (!this.m_selectedInput.SCSTuningUptoFESDone)
				{
					this.OptimizeSCSUptoFES(collatedData);
				}
				else
				{
					this.OptimizeSCSUptoUniqueStmt(collatedData);
				}
			}
			else
			{
				int num = (OracleTuner.m_MaxStatementCacheSize < this.m_selectedInput.MaxAllowedCursors) ? OracleTuner.m_MaxStatementCacheSize : this.m_selectedInput.MaxAllowedCursors;
				if (this.m_selectedInput.m_RecommendedSCS != num)
				{
					this.m_selectedInput.m_RecommendedSCS = num;
				}
				this.m_selectedInput.SCSTuningUptoFESDone = true;
				this.m_selectedInput.SCSTuningUptoUniqueStmtsDone = true;
				this.m_scanCyclesToSkip = 1;
				this.m_agentState = OracleTunerState.SCAN;
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)66048, new string[0]);
			}
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x000A1260 File Offset: 0x0009F460
		private void startTunerThread()
		{
			if (this.m_tuningThread == null)
			{
				try
				{
					this.m_tuningThread = new Thread(new ThreadStart(this.TuningFunction));
					this.m_tuningThread.IsBackground = true;
					this.m_tuningThread.Start();
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.SelfTuning, new string[]
						{
							"Tuning thread started."
						});
					}
				}
				catch (Exception ex)
				{
					this.m_tuningThread = null;
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)268500992, new string[]
						{
							"Error in starting Tuning Thread: " + ex.Message
						});
					}
				}
			}
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x000A1314 File Offset: 0x0009F514
		private void ResetAllTheTuningFlags()
		{
			foreach (OracleTuner.OracleTunerInput oracleTunerInput in this.m_input.Values)
			{
				oracleTunerInput.SCSTuningUptoFESDone = false;
				oracleTunerInput.SCSTuningUptoUniqueStmtsDone = false;
			}
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x000A1374 File Offset: 0x0009F574
		private void ReleaseSamplesFromAllAgentInputs()
		{
			foreach (OracleTuner.OracleTunerInput oracleTunerInput in this.m_input.Values)
			{
				if (oracleTunerInput.m_listOfData.Count > 0)
				{
					lock (oracleTunerInput)
					{
						if (oracleTunerInput.m_listOfData.Count > 0)
						{
							oracleTunerInput.m_listOfData.Clear();
						}
					}
				}
			}
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x000A1410 File Offset: 0x0009F610
		private void SelectPoolToOptimize()
		{
			int maxDataSamplesSoFar = 0;
			OracleTuner.OracleTunerInput candidatePoolInput = this.m_selectedInput = null;
			List<OracleTuner.OracleTunerInput> source = (from ai in this.m_input.Values
			where ai.m_registered
			select ai).ToList<OracleTuner.OracleTunerInput>();
			List<OracleTuner.OracleTunerInput> list = (from ai in source
			where ai.m_listOfData.Count > 0
			select ai).ToList<OracleTuner.OracleTunerInput>();
			list.ForEach(delegate(OracleTuner.OracleTunerInput ai)
			{
				if (ai.m_noOfSubmissions > maxDataSamplesSoFar && !ai.SCSTuningUptoFESDone)
				{
					maxDataSamplesSoFar = ai.m_noOfSubmissions;
					candidatePoolInput = ai;
				}
			});
			if (candidatePoolInput == null)
			{
				maxDataSamplesSoFar = 0;
				list.ForEach(delegate(OracleTuner.OracleTunerInput ai)
				{
					if (ai.m_noOfSubmissions > maxDataSamplesSoFar && !ai.SCSTuningUptoUniqueStmtsDone)
					{
						maxDataSamplesSoFar = ai.m_noOfSubmissions;
						candidatePoolInput = ai;
					}
				});
			}
			if (candidatePoolInput == null)
			{
				if (source.Any((OracleTuner.OracleTunerInput ai) => ai.m_noOfSubmissions > 0))
				{
					this.ResetAllTheTuningFlags();
					return;
				}
			}
			else
			{
				this.m_selectedInput = candidatePoolInput.Select();
				lock (candidatePoolInput)
				{
					this.m_selectedInput.m_listOfData = candidatePoolInput.m_listOfData;
					if (!candidatePoolInput.m_bNoNeedToDisableSelfTuning)
					{
						Dictionary<string, int> collatedData = candidatePoolInput.m_collatedData;
						if (collatedData == null || collatedData.Count <= 0)
						{
							return;
						}
						OracleTuner.OracleTunerInput candidatePoolInput2 = candidatePoolInput;
						candidatePoolInput2.m_numTimesOptimized += 1;
						if (candidatePoolInput.m_numTimesOptimized <= 3)
						{
							IEnumerable<int> enumerable = from pair in collatedData
							orderby pair.Value descending
							select pair.Value;
							int num = 0;
							if (candidatePoolInput.m_listOfData != null)
							{
								num = candidatePoolInput.m_listOfData.Count * 1000;
							}
							int num2 = num / 2;
							int num3 = 0;
							foreach (int num4 in enumerable)
							{
								num2 -= num4;
								if (num2 <= 0)
								{
									break;
								}
								num3++;
							}
							int num5 = enumerable.ElementAt(num3);
							if (num5 > 2)
							{
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, OracleTraceTag.SelfTuning, new string[]
									{
										"No Need To Disable SelfTuning for Pool:" + candidatePoolInput.m_ID
									});
								}
								candidatePoolInput.m_bNoNeedToDisableSelfTuning = true;
								candidatePoolInput.m_numTimesOptimized = 0;
							}
							else if (3 == candidatePoolInput.m_numTimesOptimized)
							{
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, OracleTraceTag.SelfTuning, new string[]
									{
										"Disable SelfTuning: UnRegister Pool: " + candidatePoolInput.m_ID
									});
								}
								candidatePoolInput.m_UpdateRecommendationsDelegate(RecommendationType.Unregister, 0);
							}
						}
					}
					candidatePoolInput.m_listOfData = new List<Dictionary<string, int>>();
					candidatePoolInput.m_noOfSubmissions = 0;
				}
			}
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x000A1778 File Offset: 0x0009F978
		private void OptimizeSCSUptoFES(Dictionary<string, int> data)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)65792, new string[0]);
			}
			long avgFrequency = (data.Values.Count > 0) ? ((long)data.Values.Average()) : 0L;
			int num = data.Values.Count((int sd) => (long)sd >= avgFrequency);
			int num2 = (int)(0.2f * (float)num);
			num += 5 + ((num2 > 2) ? num2 : 2);
			int num3 = data.Count + 5;
			if (this.m_selectedInput.m_scs < num && this.m_selectedInput.m_scs < num3)
			{
				try
				{
					int num4 = (int)Math.Ceiling(0.1 * (double)(num - this.m_selectedInput.m_scs));
					if (num4 < 5)
					{
						num4 = 5;
					}
					int num5 = this.m_selectedInput.m_scs + num4;
					if (num5 > num)
					{
						num5 = num;
					}
					if (num5 > num3)
					{
						num5 = num3;
					}
					this.m_selectedInput.m_RecommendedSCS = num5;
					this.m_selectedInput.SCSTuningUptoFESDone = false;
					this.m_selectedInput.SCSTuningUptoUniqueStmtsDone = false;
					this.m_watchCyclesToSkip = 6;
					this.m_agentState = OracleTunerState.WATCH;
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)66048, new string[0]);
					}
					return;
				}
				catch (Exception ex)
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)268500992, new string[]
						{
							"OracleTuningAgent::OptimizeSCSUptoFES(): " + ex.Message
						});
					}
				}
			}
			this.m_selectedInput.SCSTuningUptoFESDone = true;
			this.m_selectedInput.SCSTuningUptoUniqueStmtsDone = false;
			this.m_scanCyclesToSkip = 1;
			this.m_agentState = OracleTunerState.SCAN;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)66048, new string[0]);
			}
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x000A1938 File Offset: 0x0009FB38
		private void OptimizeSCSUptoUniqueStmt(Dictionary<string, int> data)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)65792, new string[0]);
			}
			int num = data.Count + 5;
			if (this.m_selectedInput.m_scs < num)
			{
				try
				{
					int num2 = (int)Math.Ceiling(0.1 * (double)(num - this.m_selectedInput.m_scs));
					if (num2 < 5)
					{
						num2 = 5;
					}
					int num3 = this.m_selectedInput.m_scs + num2;
					if (num3 > num)
					{
						num3 = num;
					}
					this.m_selectedInput.m_RecommendedSCS = num3;
					this.m_selectedInput.SCSTuningUptoFESDone = true;
					this.m_selectedInput.SCSTuningUptoUniqueStmtsDone = false;
					this.m_watchCyclesToSkip = 6;
					this.m_agentState = OracleTunerState.WATCH;
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)66048, new string[0]);
					}
					return;
				}
				catch (Exception ex)
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)268500992, new string[]
						{
							"OracleTuningAgent::OptimizeSCSUptoUniqStmt(): " + ex.Message
						});
					}
					goto IL_18E;
				}
			}
			if ((double)this.m_selectedInput.m_scs <= 1.1 * (double)num)
			{
				goto IL_18E;
			}
			int num4 = (int)Math.Ceiling((double)(this.m_selectedInput.m_scs - num) * 0.1);
			if (num4 <= 0)
			{
				goto IL_18E;
			}
			int recommendedSCS = this.m_selectedInput.m_scs - num4;
			this.m_selectedInput.m_RecommendedSCS = recommendedSCS;
			this.m_selectedInput.SCSTuningUptoFESDone = true;
			this.m_selectedInput.SCSTuningUptoUniqueStmtsDone = false;
			this.m_scanCyclesToSkip = 1;
			this.m_agentState = OracleTunerState.SCAN;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)66048, new string[0]);
			}
			return;
			IL_18E:
			this.m_selectedInput.SCSTuningUptoFESDone = true;
			this.m_selectedInput.SCSTuningUptoUniqueStmtsDone = true;
			this.m_scanCyclesToSkip = 1;
			this.m_agentState = OracleTunerState.SCAN;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)66048, new string[0]);
			}
		}

		// Token: 0x04001202 RID: 4610
		private const float HIGH_MEMORY_PERCENTAGE = 0.7f;

		// Token: 0x04001203 RID: 4611
		private const float VERY_HIGH_MEMORY_PERCENTAGE = 0.8f;

		// Token: 0x04001204 RID: 4612
		private const float MIN_PERCENTAGE_RAM = 0.2f;

		// Token: 0x04001205 RID: 4613
		private const float MIN_PERCENTAGE_RAM_FOR_TUNING = 0.3f;

		// Token: 0x04001206 RID: 4614
		private const int MAX_SUBMISSIONS_TO_BE_PROCESSED = 10;

		// Token: 0x04001207 RID: 4615
		private const ushort PRIVATE_BYTES_PER_STATEMENT = 51200;

		// Token: 0x04001208 RID: 4616
		private const byte MIN_SCS_INCREMENT = 5;

		// Token: 0x04001209 RID: 4617
		private const ushort SCAN_INTERVAL = 10000;

		// Token: 0x0400120A RID: 4618
		private const ushort SUSPEND_INTERVAL = 10000;

		// Token: 0x0400120B RID: 4619
		private const ushort WAIT_INTERVAL = 5000;

		// Token: 0x0400120C RID: 4620
		private const ushort WATCH_INTERVAL = 10000;

		// Token: 0x0400120D RID: 4621
		private const byte INTERNAL_EXEC_STATEMENTS_COUNT = 5;

		// Token: 0x0400120E RID: 4622
		private const byte MIN_BUFFER_COUNT = 2;

		// Token: 0x0400120F RID: 4623
		private const float SCS_BUFFER_PERCENTAGE = 0.2f;

		// Token: 0x04001210 RID: 4624
		internal const ushort DEFAULT_STATEMENT_SAMPLES_LIMIT = 1000;

		// Token: 0x04001211 RID: 4625
		internal const float IGNORE_STATEMENT_CACHE_DECREMENT_PERCENTAGE = 0.95f;

		// Token: 0x04001212 RID: 4626
		internal const byte DEFAULT_STMT_CACHE_SIZE_WITH_SELF_TUNING = 30;

		// Token: 0x04001213 RID: 4627
		internal const byte MEDIAN_THRESHOLD_VALUE = 2;

		// Token: 0x04001214 RID: 4628
		internal const byte OPTIMIZE_COUNT = 3;

		// Token: 0x04001215 RID: 4629
		private readonly ulong m_minRAMReqdForTuning;

		// Token: 0x04001216 RID: 4630
		private readonly ulong m_minRAMNeeded;

		// Token: 0x04001217 RID: 4631
		private readonly ulong m_installedRAM;

		// Token: 0x04001218 RID: 4632
		private readonly ulong m_availableVM;

		// Token: 0x04001219 RID: 4633
		private readonly bool m_isUsableMemInfoAvail;

		// Token: 0x0400121A RID: 4634
		private readonly ulong m_highMem;

		// Token: 0x0400121B RID: 4635
		private readonly ulong m_veryHighMem;

		// Token: 0x0400121C RID: 4636
		private static readonly int m_MaxStatementCacheSize = ProviderConfig.MaxStatementCacheSize.Value;

		// Token: 0x0400121D RID: 4637
		private bool bHighMemoryAlertFlag;

		// Token: 0x0400121E RID: 4638
		private ulong m_memoryConsumptionToReduce;

		// Token: 0x0400121F RID: 4639
		private int m_watchCyclesToSkip = 1;

		// Token: 0x04001220 RID: 4640
		private int m_scanCyclesToSkip = 1;

		// Token: 0x04001221 RID: 4641
		private Thread m_tuningThread;

		// Token: 0x04001222 RID: 4642
		private object m_registrationLock = new object();

		// Token: 0x04001223 RID: 4643
		private Dictionary<string, OracleTuner.OracleTunerInput> m_input = new Dictionary<string, OracleTuner.OracleTunerInput>();

		// Token: 0x04001224 RID: 4644
		private OracleTuner.OracleTunerInput m_selectedInput;

		// Token: 0x04001225 RID: 4645
		internal static readonly IOracleTuner Instance = new OracleTuner();

		// Token: 0x02000198 RID: 408
		private static class SystemInfo
		{
			// Token: 0x06000F7E RID: 3966
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			private static extern bool GlobalMemoryStatusEx([In] [Out] OracleTuner.SystemInfo.MEMORYSTATUSEX lpBuffer);

			// Token: 0x06000F7F RID: 3967
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			private static extern void GlobalMemoryStatus([In] [Out] OracleTuner.SystemInfo.MEMORYSTATUS lpBuffer);

			// Token: 0x06000F80 RID: 3968 RVA: 0x000A1B94 File Offset: 0x0009FD94
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			public static bool getTotalVirtualAndPhysicalMemory(ref ulong virtualMemory, ref ulong physicalMemory)
			{
				virtualMemory = (physicalMemory = 0UL);
				try
				{
					OracleTuner.SystemInfo.MEMORYSTATUSEX memorystatusex = new OracleTuner.SystemInfo.MEMORYSTATUSEX();
					if (OracleTuner.SystemInfo.GlobalMemoryStatusEx(memorystatusex))
					{
						physicalMemory = memorystatusex.ullTotalPhys;
						if (memorystatusex.ullTotalVirtual <= (ulong)-1073741824)
						{
							OracleTuner.SystemInfo.MEMORYSTATUS memorystatus = new OracleTuner.SystemInfo.MEMORYSTATUS();
							OracleTuner.SystemInfo.GlobalMemoryStatus(memorystatus);
							virtualMemory = (ulong)memorystatus.dwTotalVirtual;
						}
						else
						{
							virtualMemory = memorystatusex.ullTotalVirtual;
						}
					}
				}
				catch
				{
					virtualMemory = (physicalMemory = 0UL);
				}
				return virtualMemory > 0UL && physicalMemory > 0UL;
			}

			// Token: 0x06000F81 RID: 3969 RVA: 0x000A1C1C File Offset: 0x0009FE1C
			public static ulong getAvailablePhysicalMemory()
			{
				ulong result = 0UL;
				try
				{
					OracleTuner.SystemInfo.MEMORYSTATUSEX memorystatusex = new OracleTuner.SystemInfo.MEMORYSTATUSEX();
					if (OracleTuner.SystemInfo.GlobalMemoryStatusEx(memorystatusex))
					{
						result = memorystatusex.ullAvailPhys;
					}
				}
				catch
				{
					result = 0UL;
				}
				return result;
			}

			// Token: 0x170002C5 RID: 709
			// (get) Token: 0x06000F82 RID: 3970 RVA: 0x000A1C5C File Offset: 0x0009FE5C
			public static long CurrentProcessVirtualMemoryUsage
			{
				get
				{
					ConfigBaseClass.CurrentProcess.Refresh();
					return ConfigBaseClass.CurrentProcess.VirtualMemorySize64;
				}
			}

			// Token: 0x0400122D RID: 4653
			private const uint THREE_GB = 3221225472U;

			// Token: 0x02000199 RID: 409
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
			private class MEMORYSTATUSEX
			{
				// Token: 0x06000F83 RID: 3971 RVA: 0x000A1C74 File Offset: 0x0009FE74
				public MEMORYSTATUSEX()
				{
					this.dwLength = (uint)Marshal.SizeOf(typeof(OracleTuner.SystemInfo.MEMORYSTATUSEX));
				}

				// Token: 0x0400122E RID: 4654
				public uint dwLength;

				// Token: 0x0400122F RID: 4655
				public uint dwMemoryLoad;

				// Token: 0x04001230 RID: 4656
				public ulong ullTotalPhys;

				// Token: 0x04001231 RID: 4657
				public ulong ullAvailPhys;

				// Token: 0x04001232 RID: 4658
				public ulong ullTotalPageFile;

				// Token: 0x04001233 RID: 4659
				public ulong ullAvailPageFile;

				// Token: 0x04001234 RID: 4660
				public ulong ullTotalVirtual;

				// Token: 0x04001235 RID: 4661
				public ulong ullAvailVirtual;

				// Token: 0x04001236 RID: 4662
				public ulong ullAvailExtendedVirtual;
			}

			// Token: 0x0200019A RID: 410
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
			private class MEMORYSTATUS
			{
				// Token: 0x06000F84 RID: 3972 RVA: 0x000A1C94 File Offset: 0x0009FE94
				public MEMORYSTATUS()
				{
					this.dwLength = (uint)Marshal.SizeOf(typeof(OracleTuner.SystemInfo.MEMORYSTATUS));
				}

				// Token: 0x04001237 RID: 4663
				public uint dwLength;

				// Token: 0x04001238 RID: 4664
				public uint dwMemoryLoad;

				// Token: 0x04001239 RID: 4665
				public uint dwTotalPhys;

				// Token: 0x0400123A RID: 4666
				public uint dwAvailPhys;

				// Token: 0x0400123B RID: 4667
				public uint dwTotalPageFile;

				// Token: 0x0400123C RID: 4668
				public uint dwAvailPageFile;

				// Token: 0x0400123D RID: 4669
				public uint dwTotalVirtual;

				// Token: 0x0400123E RID: 4670
				public uint dwAvailVirtual;
			}
		}

		// Token: 0x0200019B RID: 411
		private class OracleTunerInput
		{
			// Token: 0x170002C6 RID: 710
			// (get) Token: 0x06000F85 RID: 3973 RVA: 0x000A1CB4 File Offset: 0x0009FEB4
			internal Dictionary<string, int> m_collatedData
			{
				get
				{
					Dictionary<string, int> collatedData = new Dictionary<string, int>();
					this.m_listOfData.ForEach(delegate(Dictionary<string, int> sample)
					{
						foreach (KeyValuePair<string, int> keyValuePair in sample)
						{
							Dictionary<string, int> collatedData;
							if (!collatedData.ContainsKey(keyValuePair.Key))
							{
								collatedData.Add(keyValuePair.Key, keyValuePair.Value);
							}
							else if (keyValuePair.Value != 0)
							{
								string key;
								(collatedData = collatedData)[key = keyValuePair.Key] = collatedData[key] + keyValuePair.Value;
							}
						}
					});
					return collatedData;
				}
			}

			// Token: 0x170002C7 RID: 711
			// (get) Token: 0x06000F86 RID: 3974 RVA: 0x000A1CF0 File Offset: 0x0009FEF0
			// (set) Token: 0x06000F87 RID: 3975 RVA: 0x000A1CF8 File Offset: 0x0009FEF8
			internal virtual int MaxAllowedCursors
			{
				get
				{
					return this.m_maxAllowedCursors;
				}
				set
				{
					this.m_maxAllowedCursors = value;
				}
			}

			// Token: 0x170002C8 RID: 712
			// (get) Token: 0x06000F88 RID: 3976 RVA: 0x000A1D04 File Offset: 0x0009FF04
			// (set) Token: 0x06000F89 RID: 3977 RVA: 0x000A1D0C File Offset: 0x0009FF0C
			internal virtual int m_RecommendedSCS
			{
				get
				{
					return this.m_scsRecommended;
				}
				set
				{
					if (value >= 0)
					{
						this.m_scsRecommended = ((value <= OracleTuner.m_MaxStatementCacheSize) ? value : OracleTuner.m_MaxStatementCacheSize);
						if (this.m_UpdateRecommendationsDelegate != null)
						{
							this.m_UpdateRecommendationsDelegate(RecommendationType.SCS, this.m_scsRecommended);
						}
					}
				}
			}

			// Token: 0x170002C9 RID: 713
			// (get) Token: 0x06000F8A RID: 3978 RVA: 0x000A1D44 File Offset: 0x0009FF44
			// (set) Token: 0x06000F8B RID: 3979 RVA: 0x000A1D4C File Offset: 0x0009FF4C
			internal virtual bool SCSTuningUptoFESDone
			{
				get
				{
					return this.m_scsTuningUptoFESDone;
				}
				set
				{
					this.m_scsTuningUptoFESDone = value;
				}
			}

			// Token: 0x170002CA RID: 714
			// (get) Token: 0x06000F8C RID: 3980 RVA: 0x000A1D58 File Offset: 0x0009FF58
			// (set) Token: 0x06000F8D RID: 3981 RVA: 0x000A1D60 File Offset: 0x0009FF60
			internal virtual bool SCSTuningUptoUniqueStmtsDone
			{
				get
				{
					return this.m_scsTuningUptoUniqueStmtsDone;
				}
				set
				{
					this.m_scsTuningUptoUniqueStmtsDone = value;
				}
			}

			// Token: 0x06000F8E RID: 3982 RVA: 0x000A1D6C File Offset: 0x0009FF6C
			internal OracleTuner.OracleTunerInput Select()
			{
				return new OracleTuner.OracleTunerInput.OracleTunerSelectedInput
				{
					source = this,
					m_ID = this.m_ID,
					m_scs = this.m_scs,
					m_noOfConnections = this.m_noOfConnections,
					m_registered = this.m_registered,
					m_scsTuningUptoFESDone = this.m_scsTuningUptoFESDone,
					m_scsTuningUptoUniqueStmtsDone = this.m_scsTuningUptoUniqueStmtsDone,
					m_UpdateRecommendationsDelegate = this.m_UpdateRecommendationsDelegate
				};
			}

			// Token: 0x0400123F RID: 4671
			internal string m_ID;

			// Token: 0x04001240 RID: 4672
			internal int m_scs;

			// Token: 0x04001241 RID: 4673
			internal int m_noOfConnections;

			// Token: 0x04001242 RID: 4674
			private int m_maxAllowedCursors = int.MaxValue;

			// Token: 0x04001243 RID: 4675
			internal List<Dictionary<string, int>> m_listOfData = new List<Dictionary<string, int>>();

			// Token: 0x04001244 RID: 4676
			internal short m_numTimesOptimized;

			// Token: 0x04001245 RID: 4677
			internal bool m_bNoNeedToDisableSelfTuning;

			// Token: 0x04001246 RID: 4678
			internal bool m_registered = true;

			// Token: 0x04001247 RID: 4679
			internal int m_noOfSubmissions;

			// Token: 0x04001248 RID: 4680
			protected int m_scsRecommended = 30;

			// Token: 0x04001249 RID: 4681
			protected bool m_scsTuningUptoFESDone;

			// Token: 0x0400124A RID: 4682
			protected bool m_scsTuningUptoUniqueStmtsDone;

			// Token: 0x0400124B RID: 4683
			internal Action<RecommendationType, int> m_UpdateRecommendationsDelegate;

			// Token: 0x0200019C RID: 412
			private class OracleTunerSelectedInput : OracleTuner.OracleTunerInput
			{
				// Token: 0x170002CB RID: 715
				// (get) Token: 0x06000F90 RID: 3984 RVA: 0x000A1E0C File Offset: 0x000A000C
				internal override int MaxAllowedCursors
				{
					get
					{
						return this.source.m_maxAllowedCursors;
					}
				}

				// Token: 0x170002CC RID: 716
				// (set) Token: 0x06000F91 RID: 3985 RVA: 0x000A1E1C File Offset: 0x000A001C
				internal override int m_RecommendedSCS
				{
					set
					{
						if (value >= 0)
						{
							this.m_scsRecommended = ((value <= OracleTuner.m_MaxStatementCacheSize) ? value : OracleTuner.m_MaxStatementCacheSize);
							if (this.source != null && this.source.m_registered)
							{
								this.source.m_RecommendedSCS = value;
							}
						}
					}
				}

				// Token: 0x170002CD RID: 717
				// (get) Token: 0x06000F92 RID: 3986 RVA: 0x000A1E5C File Offset: 0x000A005C
				// (set) Token: 0x06000F93 RID: 3987 RVA: 0x000A1E64 File Offset: 0x000A0064
				internal override bool SCSTuningUptoFESDone
				{
					get
					{
						return this.m_scsTuningUptoFESDone;
					}
					set
					{
						this.m_scsTuningUptoFESDone = value;
						if (this.source != null && this.source.m_registered)
						{
							this.source.m_scsTuningUptoFESDone = value;
						}
					}
				}

				// Token: 0x170002CE RID: 718
				// (get) Token: 0x06000F94 RID: 3988 RVA: 0x000A1E90 File Offset: 0x000A0090
				// (set) Token: 0x06000F95 RID: 3989 RVA: 0x000A1E98 File Offset: 0x000A0098
				internal override bool SCSTuningUptoUniqueStmtsDone
				{
					get
					{
						return this.m_scsTuningUptoUniqueStmtsDone;
					}
					set
					{
						this.m_scsTuningUptoUniqueStmtsDone = value;
						if (this.source != null && this.source.m_registered)
						{
							this.source.m_scsTuningUptoUniqueStmtsDone = value;
						}
					}
				}

				// Token: 0x0400124C RID: 4684
				internal OracleTuner.OracleTunerInput source;
			}
		}
	}
}
