# TMR0
When programming PIC microcontroller sometimes you need to execute a function within an interval of time without stoping the main loop, in this case the delay_ms() (or whatever the function you use to delay) is not the solution, hopefully PICs can execute an interruption each time interval (like Timer_Tick in C# for example), and this interval has to be configured using the prescaler (configured in OPTION_REG registers) and the initial valeu of timer0 (in TMR0 register)

This program helps to find calculate the initial TMR0 and Prescaler for a given frequency and time interval


Example (MikroC syntaxe): 
for a frequency of 8MHz and we want the interruption tick each 20ms we get:
• Prescaler = 256 (1:256)
• TMR0 = 100
Knowing that when the TMR0 register is written, its increment is inhebited for the following 2 instruction cycle (see the datasheet), we adjust TMR0 value by adding 2 or 3 (try and see which one suits you the best)
• Prescaler = 256 (1:256)
• TMR0 = 103

so the program is as follow, in the main function we initialize OPTION_REG, TMR0 and INTCON
void main()
{
  ... ; some other init here
  TMR0 = 103;
  OPTION_REG = 0b111; // prescaler 1:256
  INTCON.T0IE = 1; // enable timer0 interruption
  INTCON.GIE = 1; // enable global interruption flage
  while (1)
  {
   ... ; // your code here
  }
}

in the interrupt function (function executed when an interruption is fired) we first check for the flag of the timer (to know that it's the timer who caused the interruption), reinit TMR0 (yes, eachtime) then we clear the flag (see datasheet)
void Interrupt()
{
  if (T0IF)
  {
    ...; // your code here
    TMR0 = 103;
    INTCON.T0IF = 0;
  }
}
