﻿#region license
/* DMagic Orbital Science - Solar Particle Collector
 * Some Solar Particle-Specific Animation Code On Top Of DMModuleScienceAnimate
 *
 * Copyright (c) 2014, David Grandy <david.grandy@gmail.com>
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, 
 * are permitted provided that the following conditions are met:
 * 
 * 1. Redistributions of source code must retain the above copyright notice, 
 * this list of conditions and the following disclaimer.
 * 
 * 2. Redistributions in binary form must reproduce the above copyright notice, 
 * this list of conditions and the following disclaimer in the documentation and/or other materials 
 * provided with the distribution.
 * 
 * 3. Neither the name of the copyright holder nor the names of its contributors may be used 
 * to endorse or promote products derived from this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE 
 * GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF 
 * LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT 
 * OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *  
 */
#endregion

using UnityEngine;

namespace DMagic.Part_Modules
{
	class DMSolarCollector: DMModuleScienceAnimate
	{

		[KSPField]
		public string loopingAnim = null;

		public override void OnStart(PartModule.StartState state)
		{
			base.OnStart(state);
			if (string.IsNullOrEmpty(loopingAnim))
				anim = part.FindModelAnimators(loopingAnim)[0];
			if (IsDeployed)
				primaryAnimator(1f, 0f, WrapMode.Loop, loopingAnim, anim);
			base.labDataBoost = 0.5f;
		}

		public override void deployEvent()
		{
			primaryAnimator(1f, 0f, WrapMode.Loop, loopingAnim, anim);
			base.deployEvent();
		}

		public override void retractEvent()
		{
			if (anim != null && !string.IsNullOrEmpty(loopingAnim)) {
					anim[loopingAnim].normalizedTime = anim[loopingAnim].normalizedTime % 1;
					anim[loopingAnim].wrapMode = WrapMode.Clamp;
				}
			base.retractEvent();
		}

		protected override void onComplete(ScienceData data)
		{
			data.transmitValue = 0.7f;
			base.onComplete(data);
		}

		protected override void onInitialComplete(ScienceData data)
		{
			data.transmitValue = 0.7f;
			base.onInitialComplete(data);
		}

	}
}