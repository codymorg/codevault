/******************************************************************************
  * Project Euler Solutions
  * Author : Cody Morgan
  * Brief  : This is a collection of my solutions to ProjectEuler
  *          and Leetcode problems mostly done for fun
  *          This was orginally done in C# 2013 while I was deployed with the
  *          Army but I was a young and reckless programmer so now I'm doing
  *          it the right way - or at least a better way

******************************************************************************/

#include "ProjectEulerProblems.h"
#include "Utility.h"
#include "StandardIncludes.h"



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
    long largestFactor = long(sqrt(number));

    while (!isPrime)
    {

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
    return solutions.size();
  }
}