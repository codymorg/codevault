/******************************************************************************
  * Project Euler Solutions
  * Author : Cody Morgan
  * Brief  : This is a collection of my solutions to ProjectEuler
  *          and Leetcode problems mostly done for fun
  *          This was orginally done in C# 2013 while I was deployed with the
  *          Army but I was a young and reckless programmer so now I'm doing
  *          it the right way - or at least a better way

******************************************************************************/

#include "StandardIncludes.h"
#include "ProjectEulerProblems.h"
#include "Utility.h"
#include "MathUtility.h"



// actual problems
namespace ProjectEulerProblems
{
  void Euler1()
  {
    cout << "what is the sum of all factors of 3 or 5 < 1000?\n";

    int sum = 0;
    for (int i = 3; i < 1000; i++)
    {
      if ((i % 3 == 0) || (i % 5) == 0)
      {
        sum += i;
      }
    }

    cout << "answer: " << sum << "\n";
  }

  void Euler2()
  {
    cout << "what is the sum of all even fibonacci numbers less than 4000000?\n";

    unsigned num = 1;
    unsigned back1 = 2;
    unsigned back2 = 1;
    unsigned sum = 0;

    // calculate fibonacci values
    while (num < 4000000)
    {
      num = back1 + back2;
      back2 = back1;
      back1 = num;

      // sum only those that are even
      if (num % 2 == 0)
        sum += num;
    }

    cout << "answer: " << sum << "\n";
  }

  void Euler3()
  {
    cout << "what is the largest prime factor of 600851475143?\n";

    long long number = 600851475143;
    long largestFactor = long(sqrt(number));         // there's no factor greater than the sqrt of a number
    vector<long> primes = PrimeSieve(largestFactor); // get all primes that might be factors

    // check each prime to see if it's a factor
    for (long i = long(primes.size()) - 1; i >= 0; i--)
    {
      if (number % primes[i] == 0)
      {
        largestFactor = primes[i];
        break;
      }
    }

    cout << "answer: " << largestFactor << "\n";
  }

  void Euler4()
  {
    cout << "Find the largest palindrome made from the product of two 3-digit numbers.\n";

    char* palindrome[6]; // no 3 digit multiplication is greater than 6 digits

    for (unsigned factor1 = 100; factor1 < 999; factor1++)
    {
      for (unsigned factor2 = 100; factor2 < 999; factor2++)
      {
        itoa(factor1 * factor2, *palindrome, 10);
        
      }
    }
  }
}


// project management
namespace ProjectEulerProblems
{
  typedef void(*Euler)();
  std::vector<Euler> solutions =
  {
    Euler1,
    Euler2,
    Euler3,
    Euler4,

  };

  void RunEulerSolution(unsigned number)
  {
    if (number <= solutions.size())
    {
      StopWatch sw;
      sw.start();

      solutions[number - 1]();

      sw.end();
      sw.printElapsed();
    }
    else
    {
      cout << "ERROR There is no Euler " << number << " solution\n";
    }
  }

  unsigned GetTotalSolved()
  {
    return unsigned(solutions.size());
  }
}