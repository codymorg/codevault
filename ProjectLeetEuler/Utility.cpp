/******************************************************************************
  * Project Euler Solutions
  * Author : Cody Morgan
  * Brief  : contains utility funstions for doing problems these are generic 
  *          functions that just didn't fit in their own files

******************************************************************************/

#include "Utility.h"
using std::string;

#include <iostream>
using std::cout;


//***** Stopwatch Class *****//

StopWatch::StopWatch()
{}

void StopWatch::start()
{
  start_ = std::chrono::steady_clock::now();
}

void StopWatch::end()
{
  end_ = std::chrono::steady_clock::now();
}

unsigned StopWatch::getMilliseconds()
{
  return unsigned(std::chrono::duration_cast<std::chrono::milliseconds>(end_ - start_).count());
}

unsigned StopWatch::getMicroseconds()
{
  return unsigned(std::chrono::duration_cast<std::chrono::microseconds> (end_ - start_).count());

}

unsigned StopWatch::getSeconds()
{
  return unsigned(std::chrono::duration_cast<std::chrono::seconds>(end_ - start_).count());
}

void StopWatch::printElapsed(StopWatch::PrintMode mode)
{
  cout << "\nElapsed Time: ";
  switch (mode)
  {
  case StopWatch::PrintMode::PrintMilliSeconds:
    cout << getMilliseconds() << " ms\n";
    break;

  case StopWatch::PrintMode::PrintSeconds:
    cout << getSeconds() << " sec\n";
    break;

  default:
    if (getSeconds() > 0)
      cout << getSeconds() << " seconds\n";
    else if (getMilliseconds() > 0)
      cout << getMilliseconds() << " milliseconds\n";
    else
      cout << getMicroseconds() << " microseconds\n";
    break;
  }
}


//***** String Manipulation *****//


