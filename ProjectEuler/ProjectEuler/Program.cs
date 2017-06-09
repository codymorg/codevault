using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Reflection;

namespace ProjectEuler
{
    //Front end management
    class Start
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Stopwatch sw = new Stopwatch();
                while (true)
                {
                    string project = "";
                    Console.WriteLine("\nWhich project would you like to view?");
                    project = "Euler" + Console.ReadLine();
                    Console.WriteLine("\n");
                    try
                    {
                        Type type = typeof(Euler);
                        MethodInfo info = type.GetMethod(project);
                        sw.Start();
                        info.Invoke(project, new object[0]);
                        sw.Stop();
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("\nYou have entered an invalid number");
                    }
                }
                Console.WriteLine("Total Time: {0} ms", Math.Round(sw.Elapsed.TotalMilliseconds,1));
                Console.WriteLine("Press 'Q' to Quit or any other key to view another project...\n");
                System.ConsoleKeyInfo keyInfo = Console.ReadKey();
                string obj = keyInfo.KeyChar.ToString().ToUpper();
                if (obj == "Q")
                    break;
            }
        }
    }

    //Start.Main calls the corresponding Euler.EulerXX() where XX is its number
    class Euler
    {
        public static void Euler1()
        {
            int sum = 0;
            for (int i = 3; i < 1000; i++)
            {
                if ((i % 5 == 0) || (i % 3 == 0))
                    sum += i;
            }
            Console.WriteLine("Sum of all factors of 3 or 5 < 1000: {0}",sum);
        }//sum of 3||5 factors <1000
        public static void Euler2()
        {
            int num = 1, back1 = 2, back2 = 1;
            List<int> myList = new List<int>();
            myList.Add(back1);
            while (num < 4000000)
            {
                num = back1 + back2;
                back2 = back1;
                back1 = num;
                if (num % 2 == 0)
                    myList.Add(num);
            }
            foreach (int o in myList)
            {
                Console.WriteLine(o);
            }
            num = myList.Sum();
            Console.WriteLine("The sum of even fibinacci numbers < 4e6: {0}",num);
        }//fib sequence
        public static void Euler3()
        {
            long number = 600851475143;

            //largest possible factor
            long fact = 775146;
            bool p = false;
            while (p == false)
            {
                fact = Program.IsFactor(number, fact);
                if (Program.IsPrime(fact) == true)
                    p = true;
                else
                    fact = fact - 1;
            }
            Console.WriteLine("The largest prime factor of 600851475143 is: ",fact);
        }//largest prime factor
        public static void Euler4()
        {
            long num = 989989;
            bool done = false;
            string compare = "";
            while (done == false)
            {
                long factor = 999;
                num = num - 1;
                num = Program.Palin(num);
                while (factor > 99)
                {
                    factor = Program.IsFactor(num, factor);
                    compare = num.ToString();
                    if (compare.Equals(Program.Reverse(compare)) && factor > 99 && (num / factor) < 1000)
                    {
                        done = true;
                        Console.WriteLine("final: " + compare + " " + factor + " " + (num / factor));
                        break;
                    }
                    else
                        factor = factor - 1;
                }
            }
        }//palindrome with (2) 3 digit factors
        public static void Euler5()
        {
            int x = 20;
            int num = x, factor = x;
            bool done = false;
            while (done == false)
            {
                if (num % factor == 0 && factor > 0)
                {
                    factor = factor - 1;
                    //Console.WriteLine(num + " " + factor);
                }
                else
                {
                    factor = x;
                    num = num + x;
                }
                if (factor == 0)
                {
                    done = true;
                }
            }
            Console.WriteLine(num);
        }//smallest num factor [1...20]
        public static void Euler6()
        {
            //square of sum - sum of square
            long square, sum = 0, num = 1;
            while (num < 101)
            {
                square = num * num;
                sum = sum + square;
                num = num + 1;
                Console.WriteLine(sum);
            }
            num = 1;
            square = 0;
            while (num < 101)
            {
                square = square + num;
                num = num + 1;
            }
            square = square * square;
            Console.WriteLine(" " + square);
            num = square - sum;
            Console.WriteLine("The sum of squares < 100: ",num);
        }//square of sum - sum of square
        public static void Euler7()
        {
            //prime sieve find prime #X
            long prime = 10, count = 10001;
            while (Program.PrimeList(prime) < count)
            {
                prime = prime * prime;
            }
            Console.WriteLine("The {0}th prime is: {1}",count,prime);
        }//prime sieve find prime #X
        public static void Euler8()
        {
            //given a 1000 digit number find 13 adjacent numbers with highest product 
            ulong total = 1, total2 = 0, apos = 0;
            int pos = 0, poss = 0;
            string huge = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";
            string[] multiplys = new string[13];
            while (pos <= huge.Length)
            {
                while (apos < 13 && pos < huge.Length)
                {
                    multiplys[apos] = huge.Substring(pos, 1);
                    Console.WriteLine(pos + " " + apos + " " + huge.Substring(pos, 1));
                    pos = pos + 1;
                    apos = apos + 1;
                }
                if (apos > 0)
                {
                    apos = 0;
                    if (pos < huge.Length)
                    {
                        pos = pos - 12;
                    }
                    else
                        pos = 2000;
                }
                ulong[] multiply = Array.ConvertAll(multiplys, ulong.Parse);
                for (int i = 0; i < multiply.Length - 1; i += 2)
                {
                    total = total * (multiply[i] * multiply[i + 1]);
                    Console.WriteLine(multiply[i] + ":" + multiply[i + 1] + " " + total + " " + total2);
                }
                total = total * multiply[12];
                Console.WriteLine(total);
                if (total > total2)
                {
                    total2 = total;
                    total = 1;
                    poss = pos - 1;
                }
                else
                    total = 1;
            }
            Console.WriteLine("The 13 string with highest total is: ",huge.Substring(poss, 13));
            Console.WriteLine("The product is: ",total2);
        }//given a 1000 digit number find 13 adjacent numbers with highest product
        public static void Euler9()
        {
            int a = 1, b = 2, c = 997, count = 0;
            while (b > a)
            {
                while (c > b)
                {
                    //Console.WriteLine(a + " " + b + " " + c);
                    //Console.WriteLine((a * a) + " " + (b * b) + " = " + ((a * a) + (b * b)) + " " + (c * c));
                    //Console.ReadLine();
                    if ((c * c).Equals((a * a) + (b * b)))
                    {
                        Console.WriteLine(a + " " + b + " " + c + " = " + (a + b + c));
                        Console.WriteLine("true: " + (a * a) + " " + (b * b) + " = " + (c * c) + " = " + ((a * a) + (b * b)));
                        Console.WriteLine("abc: " + a * b * c);
                        Console.ReadLine();
                    }
                    else
                    {
                        //Console.WriteLine("False");
                        b = b + 1;
                        c = c - 1;
                        count = count + 1;
                    }
                }
                a = a + 1;
                b = b - count + 1;
                c = c + count - 2;
                count = 0;
            }
        }//a^2+b^2=c^2 && a+b+c=1000
        public static void Euler10()
        {
            List<long> PList = new List<long>();
            PList = Program.PrimeSeive(2000000L);
            long sum = PList.Sum();
            Console.WriteLine("The sum of all primes < 2e6: " + sum);
        }//sum of primes<2E6
        public static void Euler11()
        {
            int limit = 20, mult = 0, max = 0, count = 0;
            int[] matrix = {   08, 02, 22, 97, 38, 15, 00, 40, 00, 75, 04, 05, 07, 78, 52, 12, 50, 77, 91, 08, 
                               49, 49, 99, 40, 17, 81, 18, 57, 60, 87, 17, 40, 98, 43, 69, 48, 04, 56, 62, 00, 
                               81, 49, 31, 73, 55, 79, 14, 29, 93, 71, 40, 67, 53, 88, 30, 03, 49, 13, 36, 65, 
                               52, 70, 95, 23, 04, 60, 11, 42, 69, 24, 68, 56, 01, 32, 56, 71, 37, 02, 36, 91, 
                               22, 31, 16, 71, 51, 67, 63, 89, 41, 92, 36, 54, 22, 40, 40, 28, 66, 33, 13, 80, 
                               24, 47, 32, 60, 99, 03, 45, 02, 44, 75, 33, 53, 78, 36, 84, 20, 35, 17, 12, 50, 
                               32, 98, 81, 28, 64, 23, 67, 10, 26, 38, 40, 67, 59, 54, 70, 66, 18, 38, 64, 70, 
                               67, 26, 20, 68, 02, 62, 12, 20, 95, 63, 94, 39, 63, 08, 40, 91, 66, 49, 94, 21, 
                               24, 55, 58, 05, 66, 73, 99, 26, 97, 17, 78, 78, 96, 83, 14, 88, 34, 89, 63, 72, 
                               21, 36, 23, 09, 75, 00, 76, 44, 20, 45, 35, 14, 00, 61, 33, 97, 34, 31, 33, 95, 
                               78, 17, 53, 28, 22, 75, 31, 67, 15, 94, 03, 80, 04, 62, 16, 14, 09, 53, 56, 92, 
                               16, 39, 05, 42, 96, 35, 31, 47, 55, 58, 88, 24, 00, 17, 54, 24, 36, 29, 85, 57, 
                               86, 56, 00, 48, 35, 71, 89, 07, 05, 44, 44, 37, 44, 60, 21, 58, 51, 54, 17, 58, 
                               19, 80, 81, 68, 05, 94, 47, 69, 28, 73, 92, 13, 86, 52, 17, 77, 04, 89, 55, 40, 
                               04, 52, 08, 83, 97, 35, 99, 16, 07, 97, 57, 32, 16, 26, 26, 79, 33, 27, 98, 66, 
                               88, 36, 68, 87, 57, 62, 20, 72, 03, 46, 33, 67, 46, 55, 12, 32, 63, 93, 53, 69, 
                               04, 42, 16, 73, 38, 25, 39, 11, 24, 94, 72, 18, 08, 46, 29, 32, 40, 62, 76, 36, 
                               20, 69, 36, 41, 72, 30, 23, 88, 34, 62, 99, 69, 82, 67, 59, 85, 74, 04, 36, 16, 
                               20, 73, 35, 29, 78, 31, 90, 01, 74, 31, 49, 71, 48, 86, 81, 16, 23, 57, 05, 54, 
                               01, 70, 54, 71, 83, 51, 54, 69, 16, 92, 33, 48, 61, 43, 52, 01, 89, 19, 67, 48 };
            while (limit <= matrix.Length)
            {
                for (int i = 0; count + i <= limit - 4; i++)//horiz
                {
                    mult = matrix[count + i] * matrix[count + i + 1] * matrix[count + i + 2] * matrix[count + i + 3];
                    if (mult > max)
                        max = mult;
                    if (i + 3 < 20 && count < 337) //diag left
                    {
                        mult = matrix[count + i] * matrix[count + i + 21] * matrix[count + i + 42] * matrix[count + i + 63];
                        if (mult > max)
                            max = mult;
                    }
                    if (i >= 3 && count < 340)//diag right
                    {
                        mult = matrix[count + i] * matrix[count + i + 19] * matrix[count + i + 38] * matrix[count + i + 57];
                        if (mult > max)
                            max = mult;
                        if ((count + i) == (limit - 4))
                        {
                            for (int j = 1; j < 4; j++) //diag right last three rows
                            {
                                mult = matrix[count + i + j] * matrix[count + i + 19 + j] * matrix[count + i + 38 + j] * matrix[count + i + 57 + j];
                                if (mult > max)
                                    max = mult;
                            }
                        }
                    }
                }
                for (int i = 0; i < 340; i++)//vert
                {
                    mult = matrix[i] * matrix[i + 20] * matrix[i + 40] * matrix[i + 60];
                    if (mult > max)
                        max = mult;
                }
                Console.WriteLine("The largest product of 4 touching numbers in a given matix is: ",max);
                limit = limit + 20;
                count = count + 20;
            }
            limit = 20;
        }//in a 20x20 grid which 4 adj factors have the greatest value
        public static void Euler12()
        {
            int fibi = 1;
            int goal = 500, max = 0;
            for (int i = 0; max <= goal; i++)
            {
                fibi = fibi + 2 + i;
                max = Program.NumFactors(fibi);
            }
            Console.WriteLine("first Triangle number with 500 factors: {0}", fibi);
        }//first fibi number (row2) with 500 factors
        public static void Euler13()
        {
            string[] hugeArray = { "37107287533902102798797998220837590246510135740250", "46376937677490009712648124896970078050417018260538", "74324986199524741059474233309513058123726617309629", "91942213363574161572522430563301811072406154908250", "23067588207539346171171980310421047513778063246676", "89261670696623633820136378418383684178734361726757", "28112879812849979408065481931592621691275889832738", "44274228917432520321923589422876796487670272189318", "47451445736001306439091167216856844588711603153276", "70386486105843025439939619828917593665686757934951", "62176457141856560629502157223196586755079324193331", "64906352462741904929101432445813822663347944758178", "92575867718337217661963751590579239728245598838407", "58203565325359399008402633568948830189458628227828", "80181199384826282014278194139940567587151170094390", "35398664372827112653829987240784473053190104293586", "86515506006295864861532075273371959191420517255829", "71693888707715466499115593487603532921714970056938", "54370070576826684624621495650076471787294438377604", "53282654108756828443191190634694037855217779295145", "36123272525000296071075082563815656710885258350721", "45876576172410976447339110607218265236877223636045", "17423706905851860660448207621209813287860733969412", "81142660418086830619328460811191061556940512689692", "51934325451728388641918047049293215058642563049483", "62467221648435076201727918039944693004732956340691", "15732444386908125794514089057706229429197107928209", "55037687525678773091862540744969844508330393682126", "18336384825330154686196124348767681297534375946515", "80386287592878490201521685554828717201219257766954", "78182833757993103614740356856449095527097864797581", "16726320100436897842553539920931837441497806860984", "48403098129077791799088218795327364475675590848030", "87086987551392711854517078544161852424320693150332", "59959406895756536782107074926966537676326235447210", "69793950679652694742597709739166693763042633987085", "41052684708299085211399427365734116182760315001271", "65378607361501080857009149939512557028198746004375", "35829035317434717326932123578154982629742552737307", "94953759765105305946966067683156574377167401875275", "88902802571733229619176668713819931811048770190271", "25267680276078003013678680992525463401061632866526", "36270218540497705585629946580636237993140746255962", "24074486908231174977792365466257246923322810917141", "91430288197103288597806669760892938638285025333403", "34413065578016127815921815005561868836468420090470", "23053081172816430487623791969842487255036638784583", "11487696932154902810424020138335124462181441773470", "63783299490636259666498587618221225225512486764533", "67720186971698544312419572409913959008952310058822", "95548255300263520781532296796249481641953868218774", "76085327132285723110424803456124867697064507995236", "37774242535411291684276865538926205024910326572967", "23701913275725675285653248258265463092207058596522", "29798860272258331913126375147341994889534765745501", "18495701454879288984856827726077713721403798879715", "38298203783031473527721580348144513491373226651381", "34829543829199918180278916522431027392251122869539", "40957953066405232632538044100059654939159879593635", "29746152185502371307642255121183693803580388584903", "41698116222072977186158236678424689157993532961922", "62467957194401269043877107275048102390895523597457", "23189706772547915061505504953922979530901129967519", "86188088225875314529584099251203829009407770775672", "11306739708304724483816533873502340845647058077308", "82959174767140363198008187129011875491310547126581", "97623331044818386269515456334926366572897563400500", "42846280183517070527831839425882145521227251250327", "55121603546981200581762165212827652751691296897789", "32238195734329339946437501907836945765883352399886", "75506164965184775180738168837861091527357929701337", "62177842752192623401942399639168044983993173312731", "32924185707147349566916674687634660915035914677504", "99518671430235219628894890102423325116913619626622", "73267460800591547471830798392868535206946944540724", "76841822524674417161514036427982273348055556214818", "97142617910342598647204516893989422179826088076852", "87783646182799346313767754307809363333018982642090", "10848802521674670883215120185883543223812876952786", "71329612474782464538636993009049310363619763878039", "62184073572399794223406235393808339651327408011116", "66627891981488087797941876876144230030984490851411", "60661826293682836764744779239180335110989069790714", "85786944089552990653640447425576083659976645795096", "66024396409905389607120198219976047599490197230297", "64913982680032973156037120041377903785566085089252", "16730939319872750275468906903707539413042652315011", "94809377245048795150954100921645863754710598436791", "78639167021187492431995700641917969777599028300699", "15368713711936614952811305876380278410754449733078", "40789923115535562561142322423255033685442488917353", "44889911501440648020369068063960672322193204149535", "41503128880339536053299340368006977710650566631954", "81234880673210146739058568557934581403627822703280", "82616570773948327592232845941706525094512325230608", "22918802058777319719839450180888072429661980811197", "77158542502016545090413245809786882778948721859617", "72107838435069186155435662884062257473692284509516", "20849603980134001723930671666823555245252804609722", "53503534226472524250874054075591789781264330331690" };
            //string[] hugeArray = { "9999999999", "2222222222", "1234567890" };
            string saver = "";
            List<int> sumLst = new List<int>();
            List<string> saveLst = new List<string>();
            int character = hugeArray[0].Length - 1, summation, count = 0;
            while (character >= 0)
            {
                for (int a = 0; a < hugeArray.Length; a++)
                {
                    string numString = hugeArray[a].Substring(character, 1);
                    int numInt = int.Parse(numString);
                    sumLst.Add(numInt);
                    Console.WriteLine("array: {0} number: {1}", a, numString);
                }
                summation = sumLst.Sum();
                sumLst.Clear();
                saver = Program.Reverse(summation.ToString());
                saveLst.Add(saver.Substring(0, 1));
                Console.WriteLine("sum: {0} save:{1}", summation, saveLst[count]);
                count = count + 1;
                saver = Program.Reverse(saver).Substring(0, saver.Length - 1);
                Console.WriteLine("recycle: {0}", saver);
                //Console.ReadLine();
                summation = int.Parse(saver);
                sumLst.Add(summation);
                character = character - 1;

            }
            saveLst.Add(saver);
            saveLst.Reverse();
            foreach (string value in saveLst)
                Console.Write(value);
            Console.ReadLine();
        }//sum of 100 50digit numbers
        public static void Euler14()
        {
            uint count = 0, countMax = 0;
            uint[] cache = new uint[1000001];
            for (uint n = 4; n < cache.Length; n++)
            {
                uint i = n;
                count = 0;
                while (i > 1)
                {
                    count++;
                    if (i % 2 == 0)
                        i = i / 2;
                    else
                        i = (i * 3) + 1;
                    if (i < cache.Length)
                    {
                        if (cache[i] > 0)
                        {
                            count = count + cache[i];
                            i = 1;
                        }
                    }
                }
                if (count > countMax)
                {
                    countMax = count;
                    Console.WriteLine("{0} has {1} numbers in its series. countmax: {2} ", n, count, countMax);
                }
                if (cache[n] == 0)
                {
                    cache[n] = count;
                }
            }
        }//longest colletz sequence <1E6
        public static void Euler15()
        {
            int size = 20;
            long total = 1;
            long[][] Matrix = new long[size][];
            for (int i = 0; i < size; i++)
            {
                Matrix[i] = new long[size];
                Matrix[0][i] = 1;
                Matrix[i][0] = 1;
            }
            for (int array = 1; array < size; array++)
            {
                long previous = 0;
                for (int pos = size - 1; pos > 0; pos--)
                {
                    Matrix[array][pos] = Matrix[array - 1].Sum() - previous;
                    previous = Matrix[array - 1][pos] + previous;
                    //ViewMatrix(Matrix);
                    //Console.ReadLine();
                }
            }
            for (int array = 0; array < size; array++)
            {
                total = Matrix[array].Sum() + total;
            }
            Console.WriteLine("A square with {0} sides has a total of {1} routes from corner to corner", size, total);
            Console.ReadLine();
        }//possible routes across a x sided square
        public static void Euler16()
        {
            string total = "1", wTotal = "";
            int exp = 1000;
            for (int i = 1; i <= exp; i++)
            {
                int carry = 0, nextExp = 0;
                for (int j = 0; j < total.Length; j++)
                {
                    nextExp = Convert.ToInt16(total.Substring(total.Length - 1 - j, 1)) * 2 + carry;
                    carry = 0;
                    wTotal = nextExp.ToString().Substring(nextExp.ToString().Length - 1, 1) + wTotal;
                    if (nextExp.ToString().Length > 1)
                        carry = Convert.ToInt16(nextExp.ToString().Substring(0, nextExp.ToString().Length - 1));
                }
                if (carry > 0)
                {
                    wTotal = carry + wTotal;
                }
                total = wTotal;
                wTotal = "";
            }
            long sum = 0;
            for (int i = 0; i < total.Length; i++)
            {
                sum = sum + Convert.ToInt16(total.Substring(total.Length - 1 - i, 1));
            }
            Console.WriteLine("2^{0}={1}", exp, total);
            Console.WriteLine("Total Sum: " + sum);
        }//2^1000 accurate calculator
        public static void Euler17()
        {
            int[] onesPlace = { 0, 3, 3, 5, 4, 4, 3, 5, 5, 4, 3, 6, 6, 8, 8, 7, 7, 9, 8, 8 };
            int[] tensPlace = { 0, 0, 6, 6, 5, 5, 5, 7, 6, 6 };
            int count = onesPlace.Sum();
            for (int i = 20; i < 1000; i++)
            {
                if (i.ToString().Length == 2)
                {
                    count = count + onesPlace[Convert.ToInt16(i.ToString().Substring(1, 1))];
                    count = count + tensPlace[Convert.ToInt16(i.ToString().Substring(0, 1))];
                }
                if (i.ToString().Length == 3)
                {
                    if (i % 100 < 20)
                        count = count + onesPlace[Convert.ToInt16(i.ToString().Substring(1, 2))];
                    else
                    {
                        count = count + onesPlace[Convert.ToInt16(i.ToString().Substring(2, 1))];
                        count = count + tensPlace[Convert.ToInt16(i.ToString().Substring(1, 1))];
                    }
                    count = count + onesPlace[Convert.ToInt16(i.ToString().Substring(0, 1))] + 10;
                    if (i % 100 == 0)
                        count = count - 3;
                }
            }
            count = count + 11;
            Console.WriteLine("Total: " + count);
        }//sum of the letters in the numbers 1-1000
        public static void Euler18()
        {
            int[][] pyramid = new int[15][];
            pyramid[0]  = new int[1]  { 75 };
            pyramid[1]  = new int[2]  { 95, 64 };
            pyramid[2]  = new int[3]  { 17, 47, 82 };
            pyramid[3]  = new int[4]  { 18, 35, 87, 10 };
            pyramid[4]  = new int[5]  { 20, 04, 82, 47, 65 };
            pyramid[5]  = new int[6]  { 19, 01, 23, 75, 03, 34 };
            pyramid[6]  = new int[7]  { 88, 02, 77, 73, 07, 63, 67 };
            pyramid[7]  = new int[8]  { 99, 65, 04, 28, 06, 16, 70, 92 };
            pyramid[8]  = new int[9]  { 41, 41, 26, 56, 83, 40, 80, 70, 33 };
            pyramid[9]  = new int[10] { 41, 48, 72, 33, 47, 32, 37, 16, 94, 29 };
            pyramid[10] = new int[11] { 53, 71, 44, 65, 25, 43, 91, 52, 97, 51, 14 };
            pyramid[11] = new int[12] { 70, 11, 33, 28, 77, 73, 17, 78, 39, 68, 17, 57 };
            pyramid[12] = new int[13] { 91, 71, 52, 38, 17, 14, 91, 43, 58, 50, 27, 29, 48 };
            pyramid[13] = new int[14] { 63, 66, 04, 68, 89, 53, 67, 30, 73, 16, 69, 87, 40, 31 };
            pyramid[14] = new int[15] { 04, 62, 98, 27, 23, 09, 70, 98, 73, 93, 38, 53, 60, 04, 23 };
            int[][] pyramidCopy = pyramid;

            for (int row = pyramid.Count() - 2; row >= 0; row--) 
            {
                for (int col = 0; col < pyramid[row].Length; col++)
                {
                    int leftSum = pyramid[row][col] + pyramid[row + 1][col];
                    int rightSum = pyramid[row][col] + pyramid[row + 1][col + 1];
                    if (leftSum > rightSum)
                        pyramid[row][col] = leftSum;
                    else
                        pyramid[row][col] = rightSum;
                }
            }
            Console.WriteLine("Maximum sum possible: {0} ",pyramid[0][0]);

        }//highest plinko sum of a given number pyramid
        public static void Euler19()
        {
            int[] weekdays = { 0, 1, 0, 0, 0, 0, 0 }; //SMTWHFSa
            int[] monthOffset = { 3, 0, 3, 2, 3, 2, 3, 3, 2, 3, 2, 3 }; //JFMAMJJASOND
            int sundayCount = 0;
            for (int year = 1900; year <= 2000; year++)
            {
                for (int eachMonth = 0; eachMonth < 12; eachMonth++)
                {
                    int lastStartingDay = 0;
                    for (int i = 0; lastStartingDay == 0; i++)
                    {
                        if (weekdays[i] == 1)
                        {
                            lastStartingDay = i;
                            break;
                        }
                    }
                    weekdays[(monthOffset[eachMonth] + lastStartingDay) % 7] = 1;
                    if (monthOffset[eachMonth] == 0)//special case for feb
                    {
                        if ((year % 4 == 0 && year % 100 != 0) || (year % 100 == 0 && year % 400 == 0))//is a leap year
                        {
                            weekdays[(monthOffset[eachMonth] + lastStartingDay) % 7] = 0;
                            weekdays[((monthOffset[eachMonth] + lastStartingDay) + 1) % 7] = 1;
                        }
                    }
                    else
                    {
                        weekdays[lastStartingDay] = 0;
                    }
                    if (weekdays[0] == 1 && year > 1900)
                        sundayCount++;
                }
            }
            Console.WriteLine("There are {0} Sunday's from 1901-2000", sundayCount);
        }//counts number of sundays from 1901-2000
        public static void Euler20()
        {
            string product = "1000";
            string proWrite = product;
            int sum = 0;
            for (int i = Convert.ToInt16(product) - 1; i > 0; i--)
            {
                product = Program.Calculate(product, (i).ToString());
                Console.WriteLine("On number: " + i);
            }
            for (int i = 0; i < product.Length; i++)
                sum += Convert.ToInt16(product.Substring(i, 1));
            Console.WriteLine("{2}!={0}\nThe sum of those digits={1}\n", product, sum, proWrite);
        }//the sum of the digits in the answer of 100(0)!
        public static void Euler21()
        {
            long number = 10000;
            long start = Convert.ToInt32(Math.Sqrt(Convert.ToDouble(number)));
            List<long> amicables = new List<long>();
            Console.WriteLine("Calculating...");
            long sum;
            while (number > start)
            {
                List<long> factors = new List<long>(Program.FactorList(number));
                sum = factors.Sum() + 1;
                List<long> factors2 = new List<long>(Program.FactorList(sum));
                long sum2 = factors2.Sum() + 1;
                if (sum2 == number && !amicables.Contains(sum2) && sum != sum2)
                {
                    amicables.Add(number);
                    amicables.Add(sum);
                }
                number--;
            }
            sum = amicables.Sum();
            foreach (long value in amicables)
            {
                Console.WriteLine("The list of Amicable factor pairs:{0}", value);
            }
            Console.WriteLine("\nThe sum of which is: {0}", sum);
        }//factor sum of a=f factor && sum of f=a && f!=a for num<=1E4
        public static void Euler22()
        {
            string[] input = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Input.txt");
            List<string> alpha = input.ToList();
            alpha.Sort();
            string answer = "0";
            for (int i = 0; i < alpha.Count; i++)
            {
                int sum = 0;
                foreach (char value in alpha[i])//name value
                {
                    sum += value - 64;
                }
                sum *= i + 1;
                answer = Program.Calculate(answer, sum.ToString(), "+");
            }
            Console.WriteLine(answer);
        }//INPUT from Input.txt each name alphabetized. the value of each name*position in list sum
        public static void Euler23()
        {
            int max = 28123;
            List<long> ampleNumbers = new List<long>();
            long finalSum = 0;
            Console.Write("Calculating");
            for (int i = 12; i <= max; i++)//finds all the ample numbs
            {
                List<long> factorList = Program.FactorList((long)i);
                if (factorList.Count > 1)
                {
                    if (factorList[factorList.Count - 1] == factorList[factorList.Count - 2])//finds doubles
                        factorList[factorList.Count - 1] = 0;
                }
                if (factorList.Sum() + 1 > i)
                    ampleNumbers.Add(i);
            }
            long[] sums = new long[max + 1];
            for (int upper = 0; upper < ampleNumbers.Count - 1; upper++)
            {
                if (upper % 1000 == 0)
                    Console.Write(".");
                for (int lower = 0; lower < ampleNumbers.Count - 1; lower++)
                {
                    long sum = ampleNumbers[upper] + ampleNumbers[lower];
                    if (sum < sums.Count())
                        sums[sum] = sum;
                }
            }
            for (int i = 0; i < sums.Count() - 1; i++)
            {
                if (sums[i] == 0)
                    finalSum += i;
            }
            Console.WriteLine("\nFinal sum of all non abundant numbers: " + finalSum);
        }//sum of all numbers that cannot be the sums of two abundant numbers (the sum of its factors is greater than the number)
        public static void Euler24()
        {
            List<int> sequence = new List<int>();
            List<int> lexicon = new List<int>();
            int max = 9;
            int line = 1000000;
            int currentMax = max;
            for (int i = 0; i <= max; i++)
            {
                sequence.Add(i);
            }
            while (lexicon.Count() < max)
            {

                List<int> current = new List<int>();
                int factoral = 1;
                for (int i = currentMax; i > 1; i--)//max!
                {
                    factoral *= i;
                }
                currentMax--;
                foreach (int value in sequence)
                {
                    if (!lexicon.Contains(value))
                        current.Add(value);
                }
                int position = Program.RoundDown(line / factoral) % (current.Count());
                if (line % 2 == 0)//if it's even special case
                {
                    if (factoral == 2)
                        position--;
                    if (factoral == 1)
                        position++;
                }
                else
                {
                    if (factoral == 1)
                        position--;
                }
                lexicon.Add(current[position]);
            }
            foreach (int value in sequence)
            {
                if (!lexicon.Contains(value))
                    lexicon.Add(value);
            }
            Console.WriteLine();
            foreach (int value in lexicon)
            {
                Console.Write(value);
            }
            Console.WriteLine();
        }//1E6th lexicon permutation of digits 0:9
        public static void Euler25()
        {
            string fib1 = "1";
            string fib2 = "1";
            string fib3 = "";
            int index = 2;
            while (fib3.Length < 1000)
            {
                index++;
                fib3 = Program.Calculate(fib1, fib2, "+");
                fib1 = fib2;
                fib2 = fib3;
                Console.WriteLine(index + ": " + fib3);
            }
        }//lib sequence firgst num>1E999
        public static void Euler26()
        {
            int sequenceLength = 0;
            int position = 0;
            for (int i = 1000; i > 1; i--) 
            {
                if (sequenceLength >= i) 
                    break;
                int[] foundRemainders = new int[i];
                int value = 1;
                position = 0;
                while (foundRemainders[value] == 0 && value != 0) 
                {
                    foundRemainders[value] = position;
                    value *= 10;
                    value %= i;
                    position++;
                }
                if (position - foundRemainders[value] > sequenceLength) 
                    sequenceLength = position - foundRemainders[value];
            }
            Console.WriteLine("1/{0} has the longest repeating cycle",position);
        }//longest repeating cycle in an irrational decimal
        public static void Euler27()
        {
            int max = 1000;
            List<int> bList = Program.PrimeSeive(max);
            List<int> pList = Program.PrimeSeive(2 * max * max + max);
            int[] highest = new int[3];
            for (int a = 1000; a >=0; a--)
            {
                for (int b = 0; b < bList.Count; b++)
			    {
                    for (int n = 0; n < 1000; n++)
			        {
                        if (Program.SeqListFind(n * n + a * n + bList[b], pList))
                        {
                            if (n > highest[2])
                                highest = new int[3] { a, bList[b], (n + 1) };
                        }
                        else
                            break;
			        }
                    for (int n = 0; n < 1000; n++)
			        {
                        if (Program.SeqListFind(n * n - a * n + bList[b], pList))
                        {
                            if (n >= highest[2])
                                highest = new int[3] { -a, bList[b], (n + 1) };
                        }
                        else
                            break;
			        }
                }
            }
            Console.WriteLine("The quadratic which generates the largest amount of primes is:\nn^2+{0}n+{1}\nThe amount of primes generated:{2}\nThe final answer to the problem a*b={3}", highest[0], highest[1], highest[2], highest[0] * highest[1]);
            

        }//largest consecutive list of primes generated from a quadratic equation
        public static void Euler28()
        {
            int firstDiagonal=3;
            int finalTotal = 1;
            for (int layer = 1; layer <= 500; layer++)
            {
                int total = (4 * firstDiagonal) + (12 * layer);
                firstDiagonal = (8 * layer) + firstDiagonal + 2;
                finalTotal += total;
            }
            Console.WriteLine("The diagonal sum= {0}", finalTotal);
        }//sum of he diagonals on a square spiral
        public static void Euler29()
        {
            SortedSet<double> set = new SortedSet<double>();
            for (double a=2;a<=100;a++)//base
                for (double b=2;b<=100;b++)//power
                    set.Add(Math.Pow(a,b));
            Console.WriteLine(set.Count());
        }//a^b unique products a&b<=100
        public static void Euler30()
        {
            List<int> answers = new List<int>();
            Console.WriteLine("What exponent are you using?");
            int exponent = Convert.ToInt16(Console.ReadLine());
            long maximum = Convert.ToInt32(Math.Pow(9, exponent));
            //determine the maximum limit
            for (long i = maximum.ToString().Length; i < (maximum * i).ToString().Length; i++ )
            {
                maximum = Convert.ToInt32(Math.Pow(9, exponent)) * i;
            }
            List<double> nDigits = new List<double>();
            for (int n = 2; n < maximum; n++)
            {
                nDigits.Clear();
                string nString = n.ToString();
                for(int digit =0; digit < nString.Count(); digit++)
                    nDigits.Add(Convert.ToDouble(nString.Substring(digit,1)));
                double power=0;
                for (int i =0; i < nDigits.Count();i++)   
                    power += Math.Pow(nDigits[i],exponent);
                if (n == power)
                {
                    answers.Add(n);
                    Console.WriteLine("{0} is number {1}", n, answers.Count);
                }
                if (n % 10000 == 0)
                    Console.WriteLine("Your maximum is {0}. You have processed {1} numbers", maximum, n);
            }
            int total = answers.Sum();
            Console.WriteLine("\n\nThe sum of all numbers in this series is: {0}\n\n",total);
        }//find sum of all: for int abc- abc=a^5+b^5+c^5
        public static void Euler31()
        {
            List<int> coins = new List<int>() {200, 100, 50, 20, 10, 5, 2, 1 };
            List<int> combo = new List<int>();
            int count = 1;
            do
            {
                //build a combo
                for (int i = 1; i < coins.Count; i++)
                {
                    //if it's starting a new combo
                    if (combo.Sum() == coins[0])
                    {
                        combo.RemoveAll(value => value == 1);

                        //moves coin counter (i) down to the next demonination
                        i = coins.FindIndex(index => index == combo[combo.Count - 1]) + 1;

                        //if the combo is X11111 the 1's are removed and X is broken down to the next smallest denomination
                        if (combo.Count == 1)
                        {
                            combo.Add(coins[coins.FindIndex(index => index == combo[0]) + 1]);
                            combo.RemoveAt(0);
                        }
                        else
                            combo.RemoveAt(combo.Count - 1);
                    }

                    //add as many of denomination as possible until the target is reached
                    while (combo.Sum() + coins[i] <= coins[0])
                    {
                        combo.Add(coins[i]);
                    }
                    if (combo.Sum() == coins[0])
                        break;
                }
                count++;
            } while (combo[0] != 1);
            Console.WriteLine("There are {0} combinations", count);
        }//find amount of combinations for x british pounds using british money
        public static void Euler32()
        {
            bool isPan = false;
            char[] digits = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string product;
            List<string> products = new List<string>();
            for (int a = 0; a <= 98; a++)
            {
                for (int b = 0; b <= 9876; b++)
                {
                    product = (a * b).ToString();
                    string c = a.ToString() + b.ToString() + product;
                    foreach(char digit in digits)
                    {
                        if(c.Contains('0') || c.Length != 9)
                        {
                            isPan = false;
                            break;
                        }
                        if (c.Contains(digit))
                            isPan = true;
                        else
                        {
                            isPan = false;
                            break;
                        }
                    }
                    if (isPan)
                    {
                        products.Add(product);
                    }
                }
            }
            List<string> uniques = products.Distinct().ToList();
            int sum = 0;
            foreach (string unique in uniques)
            {
                sum += int.Parse(unique);
            }
            Console.WriteLine("The sum is: {0}", sum);
        }//find the sum of pandigital numbers
        public static void Euler33()
        {
            List<int[]> fractions = new List<int[]>();
            for (int num = 12; num < 98; num++)
            {
                for (int den = num + 1; den < 99; den++)
                {
                    if (num == 12 && den == 34)
                        num += 0;
                    if (num % 10 != 0 && num % 11 != 0 && den % 10 != 0 && den % 11 != 0)
                    {
                        string numerator = num.ToString();
                        string denominator = den.ToString();
                        for (int n = 0; n < numerator.Length; n++)
                        {
                            for (int d = 0; d < denominator.Length; d++)
                            {
                                if (numerator[n] == denominator[d])
                                {
                                    numerator = numerator.Remove(n, 1);
                                    denominator = denominator.Remove(d, 1);
                                    decimal dec = (decimal)num / (decimal)den;
                                    if (decimal.Parse(numerator) / decimal.Parse(denominator) == dec)
                                    {
                                        fractions.Add(new int[] { num, den });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            int[] finalFraction = new int[]{1,1};
            Console.WriteLine("There are 4 fractions this method of reduction works for:\n ");
            foreach (int[] fraction in fractions)
            {
                Console.WriteLine(fraction[0] + " / " + fraction[1]);
                finalFraction[0] *= fraction[0];
                finalFraction[1] *= fraction[1];
            }
            bool isFactoring = true;
            while (isFactoring)
            {
                List<int> numFactors = Program.FactorList(finalFraction[0]);
                List<int> denFactors = Program.FactorList(finalFraction[1]);
                numFactors.Add(finalFraction[0]);
                numFactors.Sort();
                numFactors.Reverse();
                denFactors.Sort();
                denFactors.Reverse();
                foreach (int factor in numFactors)
                {
                    if (denFactors.Contains(factor))
                    {
                        finalFraction[0] /= factor;
                        finalFraction[1] /= factor;
                        isFactoring = true;
                        break;
                    }
                    else
                        isFactoring = false;
                }
                if (numFactors.Count == 0)
                {
                    isFactoring = false;
                }
            }
            Console.WriteLine("The common denominator of the product of these fractions is: {0}", finalFraction[1]);
        }
    }

    //Utility programs used by Euler methods
    class Program
    {
        public static long PrimeList(long n)
        {
            long pause = 10001;
            bool[] e = new bool[n];
            for (int i = 2; i < n; i++)
            {
                e[i] = true;//set all odds to true
            }
            //weed out the non primes by finding mutiples 
            for (int j = 2; j < n; j++)
            {
                if (e[j])//is true
                {
                    for (long p = 2; (p * j) < n; p++)
                    {
                        e[p * j] = false;
                    }
                }
            }
            long primen = -1, count = 0;
            for (int i = 0; i < e.Length; i++)
            {
                if (e[i] == true)
                {
                    count++;
                    //Console.WriteLine("Prime count: " + count + " prime: " + primen);
                    if (count >= pause)
                    {
                        primen = primen + count;
                        //Console.WriteLine("Prime count: " + count + " prime: " + primen);
                        break;
                    }
                }
                else
                {
                    primen++;
                }
            }
            return count;
        }
        public static long IsFactor(long num, long factor)
        {
            while (num % factor != 0 || num == factor)
            {
                factor = factor - 1;
                //Console.WriteLine(num + " " + factor + " " + (num % factor));

            }
            return factor;

        }//checks if factor of num is true
        public static List<int> FactorList(int num)
        {
            long num2 = num;
            List<long> factorList = FactorList(num2);
            List<int> factorList2 = new List<int>();
            foreach (long factor in factorList)
            {
                factorList2.Add(Convert.ToInt32(factor));
            }
            return factorList2;
        }
        public static List<long> FactorList(long num)
        {
            long check = 1;
            List<long> Flist = new List<long>();
            while (check <= Math.Sqrt(num))
            {
                while (num % check != 0 && check <= Math.Sqrt(num))
                {
                    check++;
                }
                if (num % check == 0 && check != 1 && check != num)
                {
                    Flist.Add(check);
                    Flist.Add(num / check);
                }
                check++;
            }
            return Flist;
        }//returns factor pairs except 1xnumber
        public static bool IsPrime(long factor)
        {
            bool p = false;
            long check = factor - 1;
            while (factor % check != 0)
            {
                check = check - 1;
               // Console.WriteLine(factor + " " + check);

                if (check == 1)
                    p = true;
            }
            return p;
        }
        public static List<long> PrimeSeive(long n)
        {
            bool[] e = new bool[n];
            for (int i = 2; i < n; i++)
            {
                e[i] = true;//set all odds to true
            }
            //weed out the non primes by finding mutiples 
            for (int j = 2; j < n; j++)
            {
                if (e[j])//is true
                {
                    for (long p = 2; (p * j) < n; p++)
                    {
                        e[p * j] = false;
                    }
                }
            }
            List<long> PList = new List<long>();
            for (long i = 0; i < e.Length; i++)
            {
                if (e[i] == true)
                {
                    //Console.WriteLine("Prime: " + i);
                    PList.Add(i);
                }
            }
            return PList;
        }
        public static List<int> PrimeSeive(int n)
        {
            bool[] e = new bool[n];
            for (int i = 2; i < n; i++)
            {
                e[i] = true;//set all odds to true
            }
            //weed out the non primes by finding mutiples 
            for (int j = 2; j < n; j++)
            {
                if (e[j])//is true
                {
                    for (long p = 2; (p * j) < n; p++)
                    {
                        e[p * j] = false;
                    }
                }
            }
            List<int> PList = new List<int>();
            for (int i = 0; i < e.Length; i++)
            {
                if (e[i] == true)
                {
                    //Console.WriteLine("Prime: " + i);
                    PList.Add(i);
                }
            }
            return PList;
        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public static long Palin(long n)
        {
            string palin = n.ToString();
            string reverse = Reverse(palin);
            while (!palin.Equals(reverse))
            {
                n = n - 1;
                palin = n.ToString();
                reverse = Reverse(palin);
            }
            long palind = Int64.Parse(palin);
            return palind;
        }
        public static int Multi(int[] array)
        {
            int total = 0;
            for (int i = 0; i < array.Length; i++)
            {
                total = total + (array[i] * array[i + 1]);
            }
            return total;
        }
        public static List<int> FibList(int max)
        {
            List<int> Flist = new List<int>();
            Flist.Add(1);
            int num = 1, back1 = 1, back2 = 1;
            while (num <= max)
            {
                Flist.Add(num);
                num = back1 + back2;
                back2 = back1;
                back1 = num;
            }
            return Flist;
        }
        public static List<long> FibSeq(int row, long num)
        {
            long fib = 1;
            int currentRow = 2;
            List<long> LowRow = new List<long>();
            LowRow.Add(1);
            List<long> HiRow = new List<long>();
            HiRow.Add(1);
            for (int i = 0; LowRow[i] <= num; i++)
            {
                fib = fib + currentRow + i;
                LowRow.Add(fib);
                HiRow.Add(HiRow[i] + LowRow[i + 1]);
            }
            if (currentRow == 2)
            {
                HiRow.Clear();
                foreach (long element in LowRow)
                    HiRow.Add(element);
            }
            currentRow = currentRow + 1;
            while (currentRow < row)
            {
                LowRow.Clear();
                foreach (long element in HiRow)
                    LowRow.Add(element);
                HiRow.Clear();
                HiRow.Add(1);
                fib = 1;
                for (int i = 0; HiRow[i] <= num; i++)
                    HiRow.Add(HiRow[i] + LowRow[i + 1]);
                currentRow = currentRow + 1;
            }
            return HiRow;
        }//input fibi row#>1 and stop number (not a count) output: list of fibi numbers
        public static int NumFactors(int input)
        {
            int check = 2, prime = 0, count = 0;
            List<int> Exp = new List<int>();
            List<int> Exp2 = new List<int>();
            while (input > 1)
            {
                while (input % check != 0)
                    check = check + 1;
                Console.WriteLine("check: {0}  \tinput: {1}", check, input);
                if (check == prime || prime == 0)
                {
                    count = count + 1;
                    input = input / check;
                    prime = check;
                    check = 2;
                }
                else
                {
                    Exp.Add(count);
                    count = 0;
                    prime = check;
                }
            }
            foreach (int num in Exp)
            {
                Exp2.Add(num + 1);
                count = 2 * Exp2.Aggregate((a, x) => a * x);
            }
            return count;
        }//returns the number of factors input has
        public static void ViewMatrix(int[][] Matrix)
        {
            for (int i = 0; i < Matrix.Length; i++)
            {
                int[] innerArray = Matrix[i];
                Console.Write(i + " ");
                for (int a = 0; a < innerArray.Length; a++)
                {
                    Console.Write(innerArray[a] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }//displays the contents of a jagged array
        public static double BinConvert(double input)
        {
            double count = 1, outp = 0, conv = 1;
            while (input >= 1)
            {
                while ((2 * conv) <= input)
                {
                    conv = conv * 2;
                    count = 10 * count;
                }
                input = input - conv;
                conv = 1;
                outp = outp + count;
                count = 1;
            }
            return outp;
        }
        public static string ListSum(List<ulong> list)
        {
            ulong rem = 0;
            string output = "";
            string[] convert = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
                convert[i] = list[i].ToString();
            for (int c = 0; c <= convert.Length; c++)
            {
                ulong sum = 0;//needs work
                for (int i = convert.Length - 1; i >= 0; i--)
                    sum = Convert.ToUInt64(convert[i].Substring(convert[i].Length - 1 - c, 1)) + rem + sum;
                string placeHold = sum.ToString().Substring(sum.ToString().Length - 1, 1);
                output = placeHold + output;
                rem = sum - Convert.ToUInt64(placeHold);
            }
            return output;
        }
        public static string Calculate(string longNumber, string other)
        {
            string zeros = "";
            string finalProduct = "";
            List<string> productList = new List<string>();
            for (int bottom = other.Length - 1; bottom >= 0; bottom--)
            {
                string product = "";
                string sum = "";
                int carry = 0;
                for (int top = longNumber.Length - 1; top >= 0; top--)//cross multiplies all the digits
                {
                    product = (Convert.ToInt16(other.Substring(bottom, 1)) * Convert.ToInt16(longNumber.Substring(top, 1)) + carry).ToString();
                    if (Convert.ToInt16(product) > 9)
                        carry = Convert.ToInt16(product.ToString().Substring(0, 1));
                    else
                    {
                        carry = 0;
                    }
                    if (top == longNumber.Length - 1)
                        sum += zeros;
                    sum = product.Substring(product.Length - 1, 1) + sum;
                    if (top == 0 && product.Length > 1)
                        sum = product.Substring(0, 1) + sum;
                }
                zeros += "0";
                productList.Add(sum);
            }
            int carries = 0;
            for (int digit = productList[productList.Count - 1].Length - 1; digit >= 0; digit--)//adds the sub products
            {
                int digitSum = 0;
                for (int i = 0; i < productList.Count; i++)
                {
                    while (productList[i].Length < productList[productList.Count - 1].Length)
                        productList[i] = "0" + productList[i];
                    digitSum += Convert.ToInt16(productList[i].Substring(digit, 1));
                }
                digitSum += carries;
                finalProduct = digitSum.ToString().Substring(digitSum.ToString().Length - 1, 1) + finalProduct;//saves the last digit
                if (digitSum > 9)
                    carries = Convert.ToInt16(digitSum.ToString().Substring(0, digitSum.ToString().Length - 1));//carries all digits except last
                else
                    carries = 0;
                if (digit == 0 && digitSum.ToString().Length > 1)
                    finalProduct = digitSum.ToString().Substring(0, 1) + finalProduct;//saves the first digit for the final string
            }
            return finalProduct;
        }//multiplies by default
        public static string Calculate(string longNumber, string other, string operand)
        {
            string output = "";
            int carry = 0;
            while (other.Length < longNumber.Length)
                other = "0" + other;
            while (other.Length > longNumber.Length)
                longNumber = "0" + longNumber;
            if (operand.Equals("+"))
            {
                for (int i = longNumber.Length - 1; i >= 0; i--)
                {
                    int digit = Convert.ToInt16(longNumber.Substring(i, 1)) + Convert.ToInt16(other.Substring(i, 1)) + carry;
                    int length = digit.ToString().Length;
                    output = digit.ToString().Substring(length - 1, 1) + output;
                    if (digit > 9)
                        carry = Convert.ToInt16(digit.ToString().Substring(0, length - 1));
                    else
                    {
                        carry = 0;
                    }
                }
                if (carry > 0)
                    output = carry + output;
            }
            if (operand == "/")
            {
                long top = Convert.ToInt32(longNumber);
                long bottom = Convert.ToInt32(other);
                int count = 0;
                List<int> total = new List<int>();
                while (top != 0 && total.Count < 300)//fractions
                {
                    int zero = -1;
                    while (bottom > top)
                    {
                        top *= 10;
                        zero++;
                    }
                    for (int i = 1; top >= bottom; i++)
                    {
                        top -= bottom;
                        count = i;
                    }
                    while (zero > 0)
                    {
                        total.Add(0);
                        zero--;
                    }
                    total.Add(count);
                    if (total.Count == 6)
                    {
                        if (total[3] == total[4] && total[4] == total[5])
                            break;
                    }
                    if (total.Count > 6)
                    {
                        if (total[0] == total[total.Count - 3] && total[1] == total[total.Count - 2] && total[2] == total.Last())
                            break;
                    }
                }
                foreach (int digit in total)
                    output += digit;
            }
            return output;
        }
        public static int RoundDown(double num)
        {
            string numString = num.ToString();
            string wholeUnit = "";
            foreach (char digit in numString)
            {
                if (digit.Equals('.'))
                    break;
                else
                {
                    wholeUnit = wholeUnit + digit;
                }
            }
            double rounded = Convert.ToDouble(wholeUnit);
            return (Convert.ToInt32(rounded));
        }
        public static bool SeqListFind(int target, List<int> list)
        {
            int check=0;
            int increase = 10;
            while(check < list.Count())
            {
                if (target <= 1)
                    return false;
                if (list[check]==target)
                    return true;
                if (list[check] > target)
                {
                    if (increase == 1)
                        return false;
                    check -= increase;
                    increase = 1;
                }
                check += increase;
            }
            return false;
        }//find a target number in a given sequential list
    }
}
    
