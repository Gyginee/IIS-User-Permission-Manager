# IIS User Permission Manager

## Introduction
This Windows Forms application is designed to automate the process of granting IIS User permissions on files within a specified directory. It's particularly useful in scenarios where an API pushes images or other files to a server folder, and IIS needs read access to these files.

## Features
- **Folder Selection**: Users can select any folder for monitoring.
- **Automatic Permission Granting**: Automatically grants IIS User permissions on new files added to the monitored folder.
- **Real-Time Monitoring**: Uses `FileSystemWatcher` to monitor the folder in real time.
- **User-Friendly Interface**: Offers a simple and intuitive graphical interface for ease of use.
- **Tray Icon Integration**: Minimizes to the system tray for unobtrusive operation.



```bash
git clone https://github.com/gyginee/IISUserPermissionManager.git
cd IISUserPermissionManager
```

## Usage

After installation, follow these steps to use the application:

1. **Start the Application**: Run the program.
2. **Choose a Folder**: Use the Folder Browser to select the folder you want to monitor.
3. **Monitor the Folder**: The application will start monitoring the folder and automatically grant IIS User permissions to any new files.
4. **View Logs**: The application logs all actions in the interface, allowing you to see which files have been modified.

## Contributing

Contributions to the IIS User Permission Manager are welcome! Here's how you can contribute:

- Fork the repository and create your branch from `main`.
- Add or improve features, fix bugs, or enhance documentation.
- Write meaningful commit messages.
- Submit a pull request with a comprehensive description of changes.

## License

This project is licensed under the MIT License - see the `LICENSE.md` file for details.

## Copyright (c) 2023 [Gyginee]

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
