//
// Gendarme.Rules.BadPractice.ToStringReturnsNullRule
//
// Authors:
//	Nidhi Rawal <sonu2404@gmail.com>
//	Sebastien Pouliot <sebastien@ximian.com>
//
// Copyright (c) <2007> Nidhi Rawal
// Copyright (C) 2008 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using Mono.Cecil;

using Gendarme.Framework;
using Gendarme.Framework.Helpers;
using Gendarme.Framework.Rocks;

namespace Gendarme.Rules.BadPractice {

	/// <summary>
	/// This rule is used for ensure that no overriden <c>ToString()</c> method returns a 
	/// null value. This makes the value more useful in debugging.
	/// </summary>
	/// <example>
	/// Bad example:
	/// <code>
	/// public override string ToString ()
	/// {
	///	return (count == 0) ? null : count.ToString ();
	/// }
	/// </code>
	/// </example>
	/// <example>
	/// Good example:
	/// <code>
	/// public override string ToString ()
	/// {
	///	return count.ToString ();
	/// }
	/// </code>
	/// </example>

	[Problem ("This type contains a ToString () method that could returns null.")]
	[Solution ("Return an empty string or other appropriate string rather than returning null.")]
	public class ToStringReturnsNullRule: ReturnNullRule, ITypeRule {

		public RuleResult CheckType (TypeDefinition type)
		{
			// rules applies to types that overrides System.Object.Equals(object)
			MethodDefinition method = type.GetMethod (MethodSignatures.ToString);
			if ((method == null) || !method.HasBody)
				return RuleResult.DoesNotApply;

			// call base class to detect if the method can return null
			return CheckMethod (method);
		}
	}
}
