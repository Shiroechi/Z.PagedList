using System;
using System.Collections;
using System.Collections.Generic;

namespace Z.PagedList
{
	/// <summary>
	/// Represents a subset of a collection of objects that can be individually accessed by index and containing
	/// metadata about the superset collection of objects this subset was created from.
	/// </summary>
	/// <remarks>
	/// Represents a subset of a collection of objects that can be individually accessed by index and containing
	/// metadata about the superset collection of objects this subset was created from.
	/// </remarks>
	/// <typeparam name = "T">The type of object the collection should contain.</typeparam>
	/// <seealso cref = "IPagedList{T}" />
	/// <seealso cref = "List{T}" />
	public abstract class BasePagedList<T> : PagedListMetaData, IPagedList<T>
	{
		protected readonly List<T> Subset = new List<T>();

		/// <summary>
		/// Parameterless constructor.
		/// </summary>
		protected internal BasePagedList()
		{
		}

		/// <summary>
		/// Initializes a new instance of a type deriving from <see cref = "BasePagedList{T}" /> and sets properties
		/// needed to calculate position and size data on the subset and superset.
		/// </summary>
		/// <param name = "pageNumber">The one-based index of the subset of objects contained by this instance.</param>
		/// <param name = "pageSize">The maximum size of any individual subset.</param>
		/// <param name = "totalItemCount">The size of the superset.</param>
		protected internal BasePagedList(int pageNumber, int pageSize, int totalItemCount)
		{
			if (pageNumber < 1)
			{
				throw new ArgumentOutOfRangeException($"pageNumber = {pageNumber}. PageNumber cannot be below 1.");
			}

			if (pageSize < 1)
			{
				throw new ArgumentOutOfRangeException($"pageSize = {pageSize}. PageSize cannot be less than 1.");
			}

			if (totalItemCount < 0)
			{
				throw new ArgumentOutOfRangeException($"totalItemCount = {totalItemCount}. TotalItemCount cannot be less than 0.");
			}

			// set source to blank list if superset is null to prevent exceptions
			this.TotalItemCount = totalItemCount;

			this.PageSize = pageSize;
			this.PageNumber = pageNumber;
			this.PageCount = this.TotalItemCount > 0
							? (int)Math.Ceiling(this.TotalItemCount / (double)this.PageSize)
							: 0;

			var pageNumberIsGood = this.PageCount > 0 && this.PageNumber <= this.PageCount;

			this.HasPreviousPage = pageNumberIsGood && this.PageNumber > 1;
			this.HasNextPage = pageNumberIsGood && this.PageNumber < this.PageCount;
			this.IsFirstPage = pageNumberIsGood && this.PageNumber == 1;
			this.IsLastPage = pageNumberIsGood && this.PageNumber == this.PageCount;

			var numberOfFirstItemOnPage = (this.PageNumber - 1) * this.PageSize + 1;

			this.FirstItemOnPage = pageNumberIsGood ? numberOfFirstItemOnPage : 0;

			var numberOfLastItemOnPage = numberOfFirstItemOnPage + this.PageSize - 1;

			this.LastItemOnPage = pageNumberIsGood
								? (numberOfLastItemOnPage > this.TotalItemCount
								   ? this.TotalItemCount
								   : numberOfLastItemOnPage)
								: 0;
		}

		#region IPagedList<T> Members

		/// <summary>
		/// 	Returns an enumerator that iterates through the BasePagedList&lt;T&gt;.
		/// </summary>
		/// <returns>A BasePagedList&lt;T&gt;.Enumerator for the BasePagedList&lt;T&gt;.</returns>
		public IEnumerator<T> GetEnumerator()
		{
			return this.Subset.GetEnumerator();
		}

		/// <summary>
		/// 	Returns an enumerator that iterates through the BasePagedList&lt;T&gt;.
		/// </summary>
		/// <returns>A BasePagedList&lt;T&gt;.Enumerator for the BasePagedList&lt;T&gt;.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		///<summary>
		///	Gets the element at the specified index.
		///</summary>
		///<param name = "index">The zero-based index of the element to get.</param>
		public T this[int index] => this.Subset[index];

		/// <summary>
		/// 	Gets the number of elements contained on this page.
		/// </summary>
		public virtual int Count => this.Subset.Count;

		///<summary>
		/// Gets a non-enumerable copy of this paged list.
		///</summary>
		///<returns>A non-enumerable copy of this paged list.</returns>
		public PagedListMetaData GetMetaData()
		{
			return new PagedListMetaData(this);
		}

		#endregion
	}
}