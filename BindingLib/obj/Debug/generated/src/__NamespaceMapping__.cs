using System;

[assembly:global::Android.Runtime.NamespaceMapping (Java = "cn.pda.serialport", Managed="CN.Pda.Serialport")]
[assembly:global::Android.Runtime.NamespaceMapping (Java = "com.handheld.uhfr", Managed="Com.Handheld.Uhfr")]
[assembly:global::Android.Runtime.NamespaceMapping (Java = "com.uhf.api.cls", Managed="Com.Uhf.Api.Cls")]

delegate int _JniMarshal_PP_I (IntPtr jnienv, IntPtr klass);
delegate IntPtr _JniMarshal_PP_L (IntPtr jnienv, IntPtr klass);
delegate short _JniMarshal_PP_S (IntPtr jnienv, IntPtr klass);
delegate void _JniMarshal_PP_V (IntPtr jnienv, IntPtr klass);
delegate bool _JniMarshal_PP_Z (IntPtr jnienv, IntPtr klass);
delegate IntPtr _JniMarshal_PPBL_L (IntPtr jnienv, IntPtr klass, sbyte p0, IntPtr p1);
delegate IntPtr _JniMarshal_PPCILILS_L (IntPtr jnienv, IntPtr klass, char p0, int p1, IntPtr p2, int p3, IntPtr p4, short p5);
delegate IntPtr _JniMarshal_PPCILILSLIIZ_L (IntPtr jnienv, IntPtr klass, char p0, int p1, IntPtr p2, int p3, IntPtr p4, short p5, IntPtr p6, int p7, int p8, bool p9);
delegate int _JniMarshal_PPI_I (IntPtr jnienv, IntPtr klass, int p0);
delegate void _JniMarshal_PPI_V (IntPtr jnienv, IntPtr klass, int p0);
delegate IntPtr _JniMarshal_PPIBSLS_L (IntPtr jnienv, IntPtr klass, int p0, sbyte p1, short p2, IntPtr p3, short p4);
delegate IntPtr _JniMarshal_PPICIIIIISLL_L (IntPtr jnienv, IntPtr klass, int p0, char p1, int p2, int p3, int p4, int p5, int p6, short p7, IntPtr p8, IntPtr p9);
delegate IntPtr _JniMarshal_PPICIILLS_L (IntPtr jnienv, IntPtr klass, int p0, char p1, int p2, int p3, IntPtr p4, IntPtr p5, short p6);
delegate IntPtr _JniMarshal_PPICILILS_L (IntPtr jnienv, IntPtr klass, int p0, char p1, int p2, IntPtr p3, int p4, IntPtr p5, short p6);
delegate IntPtr _JniMarshal_PPII_L (IntPtr jnienv, IntPtr klass, int p0, int p1);
delegate int _JniMarshal_PPIIBSLS_I (IntPtr jnienv, IntPtr klass, int p0, int p1, sbyte p2, short p3, IntPtr p4, short p5);
delegate int _JniMarshal_PPIICIIIIISLLL_I (IntPtr jnienv, IntPtr klass, int p0, int p1, char p2, int p3, int p4, int p5, int p6, int p7, short p8, IntPtr p9, IntPtr p10, IntPtr p11);
delegate int _JniMarshal_PPIICIILLS_I (IntPtr jnienv, IntPtr klass, int p0, int p1, char p2, int p3, int p4, IntPtr p5, IntPtr p6, short p7);
delegate int _JniMarshal_PPIICILILS_I (IntPtr jnienv, IntPtr klass, int p0, int p1, char p2, int p3, IntPtr p4, int p5, IntPtr p6, short p7);
delegate int _JniMarshal_PPIII_I (IntPtr jnienv, IntPtr klass, int p0, int p1, int p2);
delegate int _JniMarshal_PPIIIIILLS_I (IntPtr jnienv, IntPtr klass, int p0, int p1, int p2, int p3, int p4, IntPtr p5, IntPtr p6, short p7);
delegate int _JniMarshal_PPIIIIILS_I (IntPtr jnienv, IntPtr klass, int p0, int p1, int p2, int p3, int p4, IntPtr p5, short p6);
delegate IntPtr _JniMarshal_PPIIIILLS_L (IntPtr jnienv, IntPtr klass, int p0, int p1, int p2, int p3, IntPtr p4, IntPtr p5, short p6);
delegate IntPtr _JniMarshal_PPIIIILS_L (IntPtr jnienv, IntPtr klass, int p0, int p1, int p2, int p3, IntPtr p4, short p5);
delegate int _JniMarshal_PPIIIIS_I (IntPtr jnienv, IntPtr klass, int p0, int p1, int p2, int p3, short p4);
delegate int _JniMarshal_PPIIILL_I (IntPtr jnienv, IntPtr klass, int p0, int p1, int p2, IntPtr p3, IntPtr p4);
delegate int _JniMarshal_PPIIILLLLS_I (IntPtr jnienv, IntPtr klass, int p0, int p1, int p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6, short p7);
delegate IntPtr _JniMarshal_PPIIILLS_L (IntPtr jnienv, IntPtr klass, int p0, int p1, int p2, IntPtr p3, IntPtr p4, short p5);
delegate IntPtr _JniMarshal_PPIIILSLIIZ_L (IntPtr jnienv, IntPtr klass, int p0, int p1, int p2, IntPtr p3, short p4, IntPtr p5, int p6, int p7, bool p8);
delegate IntPtr _JniMarshal_PPIIIS_L (IntPtr jnienv, IntPtr klass, int p0, int p1, int p2, short p3);
delegate int _JniMarshal_PPIIL_I (IntPtr jnienv, IntPtr klass, int p0, int p1, IntPtr p2);
delegate int _JniMarshal_PPIILI_I (IntPtr jnienv, IntPtr klass, int p0, int p1, IntPtr p2, int p3);
delegate int _JniMarshal_PPIILILS_I (IntPtr jnienv, IntPtr klass, int p0, int p1, IntPtr p2, int p3, IntPtr p4, short p5);
delegate IntPtr _JniMarshal_PPIILL_L (IntPtr jnienv, IntPtr klass, int p0, int p1, IntPtr p2, IntPtr p3);
delegate IntPtr _JniMarshal_PPIILLLLS_L (IntPtr jnienv, IntPtr klass, int p0, int p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, short p6);
delegate int _JniMarshal_PPIILS_I (IntPtr jnienv, IntPtr klass, int p0, int p1, IntPtr p2, short p3);
delegate int _JniMarshal_PPIISSLL_I (IntPtr jnienv, IntPtr klass, int p0, int p1, short p2, short p3, IntPtr p4, IntPtr p5);
delegate int _JniMarshal_PPIL_I (IntPtr jnienv, IntPtr klass, int p0, IntPtr p1);
delegate IntPtr _JniMarshal_PPIL_L (IntPtr jnienv, IntPtr klass, int p0, IntPtr p1);
delegate IntPtr _JniMarshal_PPILI_L (IntPtr jnienv, IntPtr klass, int p0, IntPtr p1, int p2);
delegate int _JniMarshal_PPILII_I (IntPtr jnienv, IntPtr klass, int p0, IntPtr p1, int p2, int p3);
delegate IntPtr _JniMarshal_PPILILS_L (IntPtr jnienv, IntPtr klass, int p0, IntPtr p1, int p2, IntPtr p3, short p4);
delegate int _JniMarshal_PPILISL_I (IntPtr jnienv, IntPtr klass, int p0, IntPtr p1, int p2, short p3, IntPtr p4);
delegate int _JniMarshal_PPILISLL_I (IntPtr jnienv, IntPtr klass, int p0, IntPtr p1, int p2, short p3, IntPtr p4, IntPtr p5);
delegate IntPtr _JniMarshal_PPILLL_L (IntPtr jnienv, IntPtr klass, int p0, IntPtr p1, IntPtr p2, IntPtr p3);
delegate IntPtr _JniMarshal_PPILS_L (IntPtr jnienv, IntPtr klass, int p0, IntPtr p1, short p2);
delegate IntPtr _JniMarshal_PPISSL_L (IntPtr jnienv, IntPtr klass, int p0, short p1, short p2, IntPtr p3);
delegate IntPtr _JniMarshal_PPL_L (IntPtr jnienv, IntPtr klass, IntPtr p0);
delegate void _JniMarshal_PPL_V (IntPtr jnienv, IntPtr klass, IntPtr p0);
delegate IntPtr _JniMarshal_PPLI_L (IntPtr jnienv, IntPtr klass, IntPtr p0, int p1);
delegate int _JniMarshal_PPLII_I (IntPtr jnienv, IntPtr klass, IntPtr p0, int p1, int p2);
delegate IntPtr _JniMarshal_PPLII_L (IntPtr jnienv, IntPtr klass, IntPtr p0, int p1, int p2);
delegate bool _JniMarshal_PPLIIZ_Z (IntPtr jnienv, IntPtr klass, IntPtr p0, int p1, int p2, bool p3);
delegate IntPtr _JniMarshal_PPLIL_L (IntPtr jnienv, IntPtr klass, IntPtr p0, int p1, IntPtr p2);
delegate void _JniMarshal_PPLIL_V (IntPtr jnienv, IntPtr klass, IntPtr p0, int p1, IntPtr p2);
delegate IntPtr _JniMarshal_PPLISL_L (IntPtr jnienv, IntPtr klass, IntPtr p0, int p1, short p2, IntPtr p3);
delegate IntPtr _JniMarshal_PPLISLL_L (IntPtr jnienv, IntPtr klass, IntPtr p0, int p1, short p2, IntPtr p3, IntPtr p4);
delegate IntPtr _JniMarshal_PPLL_L (IntPtr jnienv, IntPtr klass, IntPtr p0, IntPtr p1);
delegate void _JniMarshal_PPLL_V (IntPtr jnienv, IntPtr klass, IntPtr p0, IntPtr p1);
delegate int _JniMarshal_PPLLI_I (IntPtr jnienv, IntPtr klass, IntPtr p0, IntPtr p1, int p2);
delegate void _JniMarshal_PPLLI_V (IntPtr jnienv, IntPtr klass, IntPtr p0, IntPtr p1, int p2);
delegate void _JniMarshal_PPLLL_V (IntPtr jnienv, IntPtr klass, IntPtr p0, IntPtr p1, IntPtr p2);
delegate IntPtr _JniMarshal_PPLLLS_L (IntPtr jnienv, IntPtr klass, IntPtr p0, IntPtr p1, IntPtr p2, short p3);
delegate IntPtr _JniMarshal_PPLLLSLIIZ_L (IntPtr jnienv, IntPtr klass, IntPtr p0, IntPtr p1, IntPtr p2, short p3, IntPtr p4, int p5, int p6, bool p7);
delegate IntPtr _JniMarshal_PPLLS_L (IntPtr jnienv, IntPtr klass, IntPtr p0, IntPtr p1, short p2);
delegate IntPtr _JniMarshal_PPLLSLIIZ_L (IntPtr jnienv, IntPtr klass, IntPtr p0, IntPtr p1, short p2, IntPtr p3, int p4, int p5, bool p6);
delegate IntPtr _JniMarshal_PPLS_L (IntPtr jnienv, IntPtr klass, IntPtr p0, short p1);
delegate IntPtr _JniMarshal_PPLSLIIZ_L (IntPtr jnienv, IntPtr klass, IntPtr p0, short p1, IntPtr p2, int p3, int p4, bool p5);
delegate IntPtr _JniMarshal_PPS_L (IntPtr jnienv, IntPtr klass, short p0);
delegate void _JniMarshal_PPSI_V (IntPtr jnienv, IntPtr klass, short p0, int p1);
delegate IntPtr _JniMarshal_PPSIIIL_L (IntPtr jnienv, IntPtr klass, short p0, int p1, int p2, int p3, IntPtr p4);
delegate bool _JniMarshal_PPZ_Z (IntPtr jnienv, IntPtr klass, bool p0);
#if !NET
namespace System.Runtime.Versioning {
    [System.Diagnostics.Conditional("NEVER")]
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Enum | AttributeTargets.Event | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Module | AttributeTargets.Property | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
    internal sealed class SupportedOSPlatformAttribute : Attribute {
        public SupportedOSPlatformAttribute (string platformName) { }
    }
}
#endif

