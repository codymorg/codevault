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
#include <exception>

/****************************************************************************
    timer for programs
****************************************************************************/
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

/****************************************************************************
    Stringy stuff - also common in Euler
****************************************************************************/
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

/****************************************************************************
    Custom Exceptions
      Overflow : trigger if it's possible to overflow a datatype
                 which project Euler does a lot
****************************************************************************/
namespace Exceptions
{
#define STRINGIFY(x) #x
#define TOSTRING(x) STRINGIFY(x)
#define GET_LINE __FILE__ ":" TOSTRING(__LINE__)

  class Overflow : public std::exception
  {
  public:
    Overflow(std::string message)
    {
      message_ = "\n\n!!!!!  ERROR  !!!!!\n!!!!!  An overflow has been detected Consider using a larger data type  !!!!!\n";
      message_ += message + "\n\n";
    }

    virtual const char* what() const throw()
    {
      return  + message_.c_str();
    }

  private:
    std::string message_;
  };
}

#endif
