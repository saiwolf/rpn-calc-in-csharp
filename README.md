# RPN Calculator In C#

> This is a program that parses a RPN Notation Equation and prints
> the result. Based off of [rpn.py](https://gist.github.com/wd5gnr/68d067c3c42a2e0e9a27b083e01f7080#file-rpn-py) by [@wd5gnr](https://github.com/wd5gnr).

## Heads Up!

This is a POC! Features may be added or removed as seen fit.

## LICENSE

See the [LICENSE](./LICENSE) file. TLDR - MIT License

## Usage

```shell
RPN.Calc.Console.exe "expression to evaluate"
```

Or you can use `dotnet` if you have it installed.

```shell
cd <Checkout directory>\RPN.Calc.Console
dotnet run -- "expression to evaluate"
```

## Valid Operators

As of 6/26/2023, [RPN.cs](./RPN_Calc.Lib/RPN.cs) understands the following operators:

| Operator | Description |
|:--------:|:------------|
| + | Addition |
| - | Subtraction |
| * | Multiplication |
| / | Division |
| ^ | Power Of |
| ! | Store Top Of Stack in a tempVar |
| @ | Retrieve tempVar and push to Top Of Stack |
| ? | Dump the stack |
| & | Dump the tempVar list |