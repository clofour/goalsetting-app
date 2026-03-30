import { Outlet, NavLink } from "react-router";
import { AppShell, Burger, Group } from '@mantine/core';
import { useDisclosure } from '@mantine/hooks';
import classes from "./App.module.css";
import Logo from "@/components/Logo.jsx";

export default function App() {
    const navLinks = [
        { link: "/app/dashboard", label: "Home" },
        { link: "/app/calendar", label: "Calendar" },
        { link: "/app/goals", label: "Goals" }
    ];

    const [opened, { toggle }] = useDisclosure();

    return (
        <AppShell
            header={{ height: { base: 56, sm: 100 } }}
            navbar={{ width: 300, breakpoint: 'sm', collapsed: { mobile: !opened } }}
            padding="md"
        >
            <AppShell.Header px="sm">
                <Group h="3.5rem">
                    <Burger opened={opened} onClick={toggle} hiddenFrom="sm" size="sm" />
                    <Logo />
                </Group>
                <Group className={classes.headerNavigation} visibleFrom="sm">
                    {navLinks.map((link) => (
                        <NavLink
                            key={link.label}
                            to={link.link}
                            className={({ isActive }) =>
                                isActive ? `${classes.navigationLink} ${classes.navigationLinkActive}` : `${classes.navigationLink}`
                            }
                        >
                            {link.label}
                        </NavLink>
                    ))}
                </Group>
            </AppShell.Header>
            <AppShell.Main>
                <Outlet />
            </AppShell.Main>
        </AppShell>
    )
}