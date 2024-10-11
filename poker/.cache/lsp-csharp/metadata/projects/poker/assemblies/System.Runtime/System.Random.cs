namespace System;

/// <summary>Represents a pseudo-random number generator, which is an algorithm that produces a sequence of numbers that meet certain statistical requirements for randomness.</summary>
public class Random
{
	/// <summary>Provides a thread-safe <see cref="T:System.Random" /> instance that may be used concurrently from any thread.</summary>
	/// <returns>A <see cref="T:System.Random" /> instance.</returns>
	public static Random Shared
	{
		get
		{
			throw null;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Random" /> class using a default seed value.</summary>
	public Random()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Random" /> class, using the specified seed value.</summary>
	/// <param name="Seed">A number used to calculate a starting value for the pseudo-random number sequence. If a negative number is specified, the absolute value of the number is used.</param>
	public Random(int Seed)
	{
	}

	/// <summary>Creates an array populated with items chosen at random from the provided set of choices.</summary>
	/// <param name="choices">The items to use to populate the array.</param>
	/// <param name="length">The length of array to return.</param>
	/// <typeparam name="T">The type of array.</typeparam>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="choices" /> is empty.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="length" /> is not zero or a positive number.</exception>
	/// <returns>An array populated with random items.</returns>
	public T[] GetItems<T>(ReadOnlySpan<T> choices, int length)
	{
		throw null;
	}

	/// <summary>Fills the elements of a specified span with items chosen at random from the provided set of choices.</summary>
	/// <param name="choices">The items to use to populate the span.</param>
	/// <param name="destination">The span to be filled with items.</param>
	/// <typeparam name="T">The type of span.</typeparam>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="choices" /> is empty.</exception>
	public void GetItems<T>(ReadOnlySpan<T> choices, Span<T> destination)
	{
	}

	/// <summary>Creates an array populated with items chosen at random from the provided set of choices.</summary>
	/// <param name="choices">The items to use to populate the array.</param>
	/// <param name="length">The length of array to return.</param>
	/// <typeparam name="T">The type of array.</typeparam>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="choices" /> is empty.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="choices" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="length" /> is not zero or a positive number.</exception>
	/// <returns>An array populated with random items.</returns>
	public T[] GetItems<T>(T[] choices, int length)
	{
		throw null;
	}

	/// <summary>Returns a non-negative random integer.</summary>
	/// <returns>A 32-bit signed integer that is greater than or equal to 0 and less than <see cref="F:System.Int32.MaxValue">Int32.MaxValue</see>.</returns>
	public virtual int Next()
	{
		throw null;
	}

	/// <summary>Returns a non-negative random integer that is less than the specified maximum.</summary>
	/// <param name="maxValue">The exclusive upper bound of the random number to be generated. <paramref name="maxValue" /> must be greater than or equal to 0.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="maxValue" /> is less than 0.</exception>
	/// <returns>A 32-bit signed integer that is greater than or equal to 0, and less than <paramref name="maxValue" />; that is, the range of return values ordinarily includes 0 but not <paramref name="maxValue" />. However, if <paramref name="maxValue" /> equals 0, 0 is returned.</returns>
	public virtual int Next(int maxValue)
	{
		throw null;
	}

	/// <summary>Returns a random integer that is within a specified range.</summary>
	/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
	/// <param name="maxValue">The exclusive upper bound of the random number returned. <paramref name="maxValue" /> must be greater than or equal to <paramref name="minValue" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="minValue" /> is greater than <paramref name="maxValue" />.</exception>
	/// <returns>A 32-bit signed integer greater than or equal to <paramref name="minValue" /> and less than <paramref name="maxValue" />; that is, the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />. If <paramref name="minValue" /> equals <paramref name="maxValue" />, <paramref name="minValue" /> is returned.</returns>
	public virtual int Next(int minValue, int maxValue)
	{
		throw null;
	}

	/// <summary>Fills the elements of a specified array of bytes with random numbers.</summary>
	/// <param name="buffer">The array to be filled with random numbers.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="buffer" /> is <see langword="null" />.</exception>
	public virtual void NextBytes(byte[] buffer)
	{
	}

	/// <summary>Fills the elements of a specified span of bytes with random numbers.</summary>
	/// <param name="buffer">The array to be filled with random numbers.</param>
	public virtual void NextBytes(Span<byte> buffer)
	{
	}

	/// <summary>Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.</summary>
	/// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
	public virtual double NextDouble()
	{
		throw null;
	}

	/// <summary>Returns a non-negative random integer.</summary>
	/// <returns>A 64-bit signed integer that is greater than or equal to 0 and less than <see cref="F:System.Int64.MaxValue">Int64.MaxValue</see>.</returns>
	public virtual long NextInt64()
	{
		throw null;
	}

	/// <summary>Returns a non-negative random integer that is less than the specified maximum.</summary>
	/// <param name="maxValue">The exclusive upper bound of the random number to be generated. <paramref name="maxValue" /> must be greater than or equal to 0.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="maxValue" /> is less than 0.</exception>
	/// <returns>A 64-bit signed integer that is greater than or equal to 0, and less than <paramref name="maxValue" />; that is, the range of return values ordinarily includes 0 but not <paramref name="maxValue" />. However, if <paramref name="maxValue" /> equals 0, <paramref name="maxValue" /> is returned.</returns>
	public virtual long NextInt64(long maxValue)
	{
		throw null;
	}

	/// <summary>Returns a random integer that is within a specified range.</summary>
	/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
	/// <param name="maxValue">The exclusive upper bound of the random number returned. <paramref name="maxValue" /> must be greater than or equal to <paramref name="minValue" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="minValue" /> is greater than <paramref name="maxValue" />.</exception>
	/// <returns>A 64-bit signed integer greater than or equal to <paramref name="minValue" /> and less than <paramref name="maxValue" />; that is, the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />. If minValue equals <paramref name="maxValue" />, <paramref name="minValue" /> is returned.</returns>
	public virtual long NextInt64(long minValue, long maxValue)
	{
		throw null;
	}

	/// <summary>Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.</summary>
	/// <returns>A single-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
	public virtual float NextSingle()
	{
		throw null;
	}

	/// <summary>Returns a random floating-point number between 0.0 and 1.0.</summary>
	/// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
	protected virtual double Sample()
	{
		throw null;
	}

	/// <summary>Performs an in-place shuffle of a span.</summary>
	/// <param name="values">The span to shuffle.</param>
	/// <typeparam name="T">The type of span.</typeparam>
	public void Shuffle<T>(Span<T> values)
	{
	}

	/// <summary>Performs an in-place shuffle of an array.</summary>
	/// <param name="values">The array to shuffle.</param>
	/// <typeparam name="T">The type of array.</typeparam>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="values" /> is <see langword="null" />.</exception>
	public void Shuffle<T>(T[] values)
	{
	}
}
