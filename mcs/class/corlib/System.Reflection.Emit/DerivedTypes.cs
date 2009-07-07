//
// System.Reflection.Emit.DerivedTypes.cs
//
// Authors:
// 	Rodrigo Kumpera <rkumpera@novell.com>
//
//
// Copyright (C) 2009 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.Reflection;
using System.Reflection.Emit;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;


namespace System.Reflection.Emit
{
	internal enum TypeKind : int {
		SZARRAY = 0x1d,
		ARRAY = 0x14
	}

	internal abstract class DerivedType : Type
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		internal static extern void create_unmanaged_type (Type type);
	
	}

	internal class ArrayType : DerivedType
	{
		Type elementType;
		int rank;

		internal ArrayType (Type elementType, int rank)
		{
			this.elementType = elementType;
			this.rank = rank;
		}

		public override Type GetInterface (string name, bool ignoreCase)
		{
			throw new NotSupportedException ();
		}

		public override Type[] GetInterfaces ()
		{
			throw new NotSupportedException ();
		}

		public override Type GetElementType ()
		{
			return elementType;
		}

		public override EventInfo GetEvent (string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException ();
		}

		public override EventInfo[] GetEvents (BindingFlags bindingAttr)
		{
			throw new NotSupportedException ();
		}

		public override FieldInfo GetField( string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException ();
		}

		public override FieldInfo[] GetFields (BindingFlags bindingAttr)
		{
			throw new NotSupportedException ();
		}

		public override MemberInfo[] GetMembers (BindingFlags bindingAttr)
		{
			throw new NotSupportedException ();
		}

		protected override MethodInfo GetMethodImpl (string name, BindingFlags bindingAttr, Binder binder,
		                                             CallingConventions callConvention, Type[] types,
		                                             ParameterModifier[] modifiers)
		{
			throw new NotSupportedException ();
		}

		public override MethodInfo[] GetMethods (BindingFlags bindingAttr)
		{
			throw new NotSupportedException ();
		}

		public override Type GetNestedType (string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException ();
		}

		public override Type[] GetNestedTypes (BindingFlags bindingAttr)
		{
			throw new NotSupportedException ();
		}

		public override PropertyInfo[] GetProperties (BindingFlags bindingAttr)
		{
			throw new NotSupportedException ();
		}

		protected override PropertyInfo GetPropertyImpl (string name, BindingFlags bindingAttr, Binder binder,
		                                                 Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException ();
		}

		protected override ConstructorInfo GetConstructorImpl (BindingFlags bindingAttr,
								       Binder binder,
								       CallingConventions callConvention,
								       Type[] types,
								       ParameterModifier[] modifiers)
		{
			throw new NotSupportedException ();
		}


		protected override TypeAttributes GetAttributeFlagsImpl ()
		{
			/*LAMEIMPL MS just return the elementType.Attributes*/
			return elementType.Attributes; 
		}

		protected override bool HasElementTypeImpl ()
		{
			return true;
		}

		protected override bool IsArrayImpl ()
		{
			return true;
		}

		protected override bool IsByRefImpl ()
		{
			return false;
		}

		protected override bool IsCOMObjectImpl ()
		{
			return false;
		}

		protected override bool IsPointerImpl ()
		{
			return false;
		}

		protected override bool IsPrimitiveImpl ()
		{
			return false;
		}


		public override ConstructorInfo[] GetConstructors (BindingFlags bindingAttr)
		{
			throw new NotSupportedException ();
		}

		public override object InvokeMember (string name, BindingFlags invokeAttr,
						     Binder binder, object target, object[] args,
						     ParameterModifier[] modifiers,
						     CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException ();
		}

		public override InterfaceMapping GetInterfaceMap (Type interfaceType)
		{
			throw new NotSupportedException ();
		}

		public override bool IsInstanceOfType (object o)
		{
			return false;
		}

		public override int GetArrayRank ()
		{
			return rank;
		}

#if NET_2_0
		//FIXME this should be handled by System.Type
		public override Type MakeGenericType (params Type[] typeArguments)
		{
			throw new NotSupportedException ();
		}

		public override Type MakeArrayType ()
		{
			return MakeArrayType (1);
		}

		public override Type MakeArrayType (int rank)
		{
			if (rank < 1)
				throw new IndexOutOfRangeException ();
			return new ArrayType (this, rank);
		}

		public override Type MakeByRefType ()
		{
			create_unmanaged_type (this);
			return base.MakeByRefType ();
		}

		public override Type MakePointerType ()
		{
			create_unmanaged_type (this);
			return base.MakePointerType ();
		}

		public override GenericParameterAttributes GenericParameterAttributes {
			get { throw new NotSupportedException (); }
		}

		public override StructLayoutAttribute StructLayoutAttribute {
			get { throw new NotSupportedException (); }
		}
#endif

		public override Assembly Assembly {
			get { return elementType.Assembly; }
		}

		public override string AssemblyQualifiedName {
			get { return FullName + ", " + elementType.Assembly.FullName; }
		}

		public override Type BaseType {
			get { return typeof (System.Array); }
		}

		public override string FullName {
			get {
				//FIXME use a StringBuilder
				String commas = "";
				for (int i = 1; i < rank; ++i)
					commas += ",";
				return String.Format("{0}[{1}]", elementType.FullName, commas);
			}
		}

		public override Guid GUID {
			get { throw new NotSupportedException (); }
		}

		public override Module Module {
			get { return elementType.Module; }
		}
	
		public override string Namespace {
			get { return elementType.Namespace; }
		}

		public override RuntimeTypeHandle TypeHandle {
			get { throw new NotSupportedException (); }
		}

		public override Type UnderlyingSystemType {
			get { return this; }
		}

		//MemberInfo
		public override bool IsDefined (Type attributeType, bool inherit)
		{
			throw new NotSupportedException ();
		}

		public override object [] GetCustomAttributes (bool inherit)
		{
			throw new NotSupportedException ();
		}

		public override object [] GetCustomAttributes (Type attributeType, bool inherit)
		{
			throw new NotSupportedException ();
		}

		public override string Name {
			get {
				//FIXME use a StringBuilder
				String commas = "";
				for (int i = 1; i < rank; ++i)
					commas += ",";
				return String.Format("{0}[{1}]", elementType.Name, commas);
			}
		}
	}
}
