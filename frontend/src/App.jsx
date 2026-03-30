import { Outlet } from "react-router";
import { AppShell, Burger, Group, Text, NavLink } from '@mantine/core';
import { useDisclosure } from '@mantine/hooks';

export default function App() {
    const navLinks = [
        { href: "/app/dashboard", label: "Home" },
        { href: "/app/calendar", label: "Calendar" },
        { href: "/app/goals", label: "Goals" }
    ];

    const [opened, { toggle }] = useDisclosure();

    return (
        <AppShell
            header={{ height: 60 }}
            navbar={{ width: 300, breakpoint: 'sm', collapsed: { mobile: !opened } }}
            padding="md"
        >
            <AppShell.Header>
                <Group h="100%" px="md">
                    <Burger opened={opened} onClick={toggle} hiddenFrom="sm" size="sm" />
                    Header has a burger icon below sm breakpoint
                </Group>
            </AppShell.Header>
            <AppShell.Navbar p="md">
                {navLinks.map((link) => (
                    <NavLink
                        label={link.label}
                        href={link.href}
                    />
                ))}
            </AppShell.Navbar>
            <AppShell.Main>
                <Outlet />
            </AppShell.Main>
        </AppShell>
    )
}