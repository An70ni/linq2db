﻿using System.Collections.Generic;
using System.Linq;
using LinqToDB.CodeModel;

namespace LinqToDB.DataModel
{
	/// <summary>
	/// Root object for database model. Contains reference to data model and various model options.
	/// </summary>
	public sealed class DatabaseModel
	{
		public DatabaseModel(DataContextModel context)
		{
			DataContext = context;
		}

		/// <summary>
		/// Gets database context descriptor.
		/// </summary>
		public DataContextModel DataContext                      { get; }

		/// <summary>
		/// Enable supporession of xml-doc build warnings (e.g. due to missing xml-doc comments) in generated code.
		/// </summary>
		public bool             DisableXmlDocWarnings            { get; set; }
		/// <summary>
		/// Enable generation of nullable annotations (NRT) in generated code.
		/// </summary>
		public bool             NRTEnabled                       { get; set; }
		/// <summary>
		/// Optional header comment text on top of each generated file. Will be wrapped into &lt;auto-generated&gt; tag.
		/// </summary>
		public string?          AutoGeneratedHeader              { get; set; }

		// options below are marked internal, as they are inherited from scaffold options, set by user
		//
		/// <summary>
		/// When specified, many-sided association property/method will use specified type as return type.
		/// Type must be open generic type with one type argument, e.g. <see cref="IEnumerable{T}"/>, <see cref="List{T}"/> or <see cref="ICollection{T}"/>.
		/// Otherwise <see cref="IQueryable{T}"/> type used.
		/// </summary>
		internal IType?         AssociationCollectionType        { get; set; }
	}
}
