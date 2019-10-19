/******************************************************************************
  * Project Euler Solutions
  * Author : Cody Morgan
  * Brief  : contains utility funstions related to math not computer science

******************************************************************************/

#pragma once
#ifndef UTILITY_MATH_H
#define UTILITY_MATH_H

#include <vector>

/******************************************************************************
  gets all primes under a given amount
  slower than IsPrime for one prime, but faster for multiple primes

******************************************************************************/
template <class IntegralType>
std::vector<IntegralType> PrimeSieve(IntegralType T)
{
  std::vector<IntegralType> primes;
  bool* sieve = new bool[T];

  // save the next prime
  for (IntegralType prime = 2; prime < T / 2; prime++)
  {
    // we can skip this number as it has already be evaluated to be not prime
    if (sieve[prime] == false)
      continue;

    primes.push_back(prime);

    // stamp each multiple of this prime number
    for (IntegralType multiple = prime; multiple < T; multiple += prime)
    {
      sieve[multiple] = false;
    }
  }

  delete[] sieve;

  return primes;
}


/******************************************************************************
  tells you if this number is prime 
  faster than Sieve for checking a single prime - not many

******************************************************************************/
template <class IntrgralType>
bool IsPrime(IntrgralType a)
{
  for (IntrgralType i = 2; i < a; i++)
  {
    if (a % i == 0)
      return false;
  }

  return true;
}


#endif
