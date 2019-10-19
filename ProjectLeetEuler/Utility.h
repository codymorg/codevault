/******************************************************************************
  * Project Euler Solutions
  * Author : Cody Morgan
  * Brief  : contains utility funstions for doing problems these are generic
  *          functions that just didn't fit in their own files

******************************************************************************/

#pragma once
#ifndef UTILITY_EULER_H
#define UTILTY_EULER_H
#include <chrono>
#include <string>

class StopWatch
{
public:
  enum class PrintMode
  {
    PrintDefault,
    PrintMilliSeconds,
    PrintSeconds,
  };

  StopWatch();
  void start();
  void end();
  unsigned getSeconds();
  unsigned getMilliseconds();
  unsigned getMicroseconds();
  void printElapsed(PrintMode mode = PrintMode::PrintDefault);
private:
  std::chrono::time_point<std::chrono::steady_clock> start_;
  std::chrono::time_point<std::chrono::steady_clock> end_;
};

namespace StringManipulation
{
  template <class charArray>
  bool IsPalindrome(const charArray& input, unsigned size)
  {
    // ignore the null terminator
    size--;
    for (unsigned i = 0; i < size / 2; i++)
    {
      if (input[i] != input[size - 1 - i])
        return false;
    }

    return true;
  }
}

#endif
