﻿/*
 * Copyright (c) <2020> Side Effects Software Inc.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice,
 *    this list of conditions and the following disclaimer.
 *
 * 2. The name of Side Effects Software may not be used to endorse or
 *    promote products derived from this software without specific prior
 *    written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY SIDE EFFECTS SOFTWARE "AS IS" AND ANY EXPRESS
 * OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.  IN
 * NO EVENT SHALL SIDE EFFECTS SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT,
 * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA,
 * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 * LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
 * EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */


using UnityEngine;
using System.Collections;

// Expose internal classes/functions
#if UNITY_EDITOR
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HoudiniEngineUnityEditor")]
[assembly: InternalsVisibleTo("HoudiniEngineUnityEditorTests")]
[assembly: InternalsVisibleTo("HoudiniEngineUnityPlayModeTests")]
#endif

namespace HoudiniEngineUnity
{
    [System.Serializable]
    internal sealed class HEU_AttributeData : IEquivable<HEU_AttributeData>
    {
        public HAPI_AttributeInfo _attributeInfo;

        public string _name;

        public enum AttributeType
        {
            UNDEFINED = -1,
            BOOL,
            INT,
            FLOAT,
            STRING,
            MAX
        }

        public AttributeType _attributeType;

        public int[] _intValues;
        public float[] _floatValues;
        public string[] _stringValues;

        public bool IsColorAttribute()
        {
            return _name.Equals(HEU_HAPIConstants.HAPI_ATTRIB_COLOR) ||
                   _attributeInfo.typeInfo == HAPI_AttributeTypeInfo.HAPI_ATTRIBUTE_TYPE_COLOR;
        }

        public enum AttributeState
        {
            INVALID, // Not in a good state. Should be re-created.
            SYNCED, // In good state and sync'd with Houdini
            LOCAL_DIRTY // Local side is dirty and requires upload to Houdini
        }

        public AttributeState _attributeState;

        internal void CopyValuesTo(HEU_AttributeData destAttrData)
        {
            if (this._intValues == null)
            {
                destAttrData._intValues = null;
            }
            else
            {
                int arraySize = this._intValues.Length;
                System.Array.Resize<int>(ref destAttrData._intValues, arraySize);
                System.Array.Copy(this._intValues, destAttrData._intValues, arraySize);
            }

            if (this._floatValues == null)
            {
                destAttrData._floatValues = null;
            }
            else
            {
                int arraySize = this._floatValues.Length;
                System.Array.Resize<float>(ref destAttrData._floatValues, arraySize);
                System.Array.Copy(this._floatValues, destAttrData._floatValues, arraySize);
            }

            if (this._stringValues == null)
            {
                destAttrData._stringValues = null;
            }
            else
            {
                int arraySize = this._stringValues.Length;
                System.Array.Resize<string>(ref destAttrData._stringValues, arraySize);
                System.Array.Copy(this._stringValues, destAttrData._stringValues, arraySize);
            }
        }

        public bool IsEquivalentTo(HEU_AttributeData other)
        {
            bool bResult = true;

            string header = "HEU_AttributeData";

            if (other == null)
            {
                HEU_Logger.LogError(header + " Not equivalent");
                return false;
            }

            HEU_TestHelpers.AssertTrueLogEquivalent(this._attributeInfo.ToTestObject(),
                other._attributeInfo.ToTestObject(), ref bResult, header, "_attributeInfo");
            HEU_TestHelpers.AssertTrueLogEquivalent(this._name, other._name, ref bResult, header, "_name");
            HEU_TestHelpers.AssertTrueLogEquivalent(this._attributeType, other._attributeType, ref bResult, header,
                "_attributeType");

            HEU_TestHelpers.AssertTrueLogEquivalent(this._intValues, other._intValues, ref bResult, header,
                "_intValues");

            HEU_TestHelpers.AssertTrueLogEquivalent(this._floatValues, other._floatValues, ref bResult, header,
                "_floatValues");

            HEU_TestHelpers.AssertTrueLogEquivalent(this._stringValues, other._stringValues, ref bResult, header,
                "_stringValues");

            HEU_TestHelpers.AssertTrueLogEquivalent(this._attributeState, other._attributeState, ref bResult, header,
                "_attributeState");

            return bResult;
        }
    }
} // HoudiniEngineUnity